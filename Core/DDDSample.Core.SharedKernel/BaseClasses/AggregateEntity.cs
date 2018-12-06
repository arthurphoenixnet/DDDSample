using DDDSample.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DDDSample.Core.SharedKernel.BaseClasses
{
    public abstract class AggregateEntity<TId> :
        IAggregate<TId>, IEventSourcingAggregate<TId>
    {
        #region Fields

        public const long NewAggregateVersion = -1;
        private readonly ICollection<IDomainEvent<TId>> _uncommittedEvents = new LinkedList<IDomainEvent<TId>>();
        private long _version = NewAggregateVersion;
        protected IDictionary<string, Action<IDomainEvent>> _evtHandlerMap = new ConcurrentDictionary<string, Action<IDomainEvent>>();

        #endregion Fields

        #region Properties

        public TId Id { get; protected set; }

        long IEventSourcingAggregate<TId>.Version => _version;

        #endregion Properties

        #region Public Methods

        void IEventSourcingAggregate<TId>.ApplyEvent(IDomainEvent<TId> @event, long version)
        {
            if (_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
                return;

            string evtTypeFullName = @event.GetType().FullName;
            if (this._evtHandlerMap.ContainsKey(evtTypeFullName) == false)
                throw new NullReferenceException("Aggregate can't refer to correct event handler");

            this._evtHandlerMap[evtTypeFullName](@event);
            _version = version;
        }

        void IEventSourcingAggregate<TId>.ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        IEnumerable<IDomainEvent<TId>> IEventSourcingAggregate<TId>.GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

        #endregion Public Methods

        #region Abstract Methods

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : DomainEvent<TId>
        {
            IDomainEvent<TId> eventWithAggregate = @event.WithAggregate(
                Equals(Id, default(TId)) ? @event.AggregateId : Id,
                _version);

            ((IEventSourcingAggregate<TId>)this).ApplyEvent(eventWithAggregate, _version + 1);
            _uncommittedEvents.Add(eventWithAggregate);
        }

        #endregion Abstract Methods
    }
}