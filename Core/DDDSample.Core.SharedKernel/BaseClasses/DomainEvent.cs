using DDDSample.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace DDDSample.Core.SharedKernel.BaseClasses
{
    public abstract class DomainEvent<TAggregateId> : IDomainEvent<TAggregateId>, IEquatable<DomainEvent<TAggregateId>>
    {
        protected DomainEvent()
        {
            this.EventId = Guid.NewGuid();
        }

        protected DomainEvent(TAggregateId aggregateId)
        {
            AggregateId = aggregateId;
        }

        protected DomainEvent(TAggregateId aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }

        public TAggregateId AggregateId { get; private set; }

        public long AggregateVersion { get; private set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as DomainEvent<TAggregateId>);
        }

        public bool Equals(DomainEvent<TAggregateId> other)
        {
            return other != null &&
                   EventId.Equals(other.EventId);
        }

        public override int GetHashCode()
        {
            return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
        }

        public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
    }
}