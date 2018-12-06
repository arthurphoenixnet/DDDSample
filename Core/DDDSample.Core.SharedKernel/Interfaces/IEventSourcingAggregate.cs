using System.Collections.Generic;

namespace DDDSample.Core.SharedKernel.Interfaces
{
    public interface IEventSourcingAggregate<TAggregateId>
    {
        long Version { get; }

        void ApplyEvent(IDomainEvent<TAggregateId> @event, long version);

        IEnumerable<IDomainEvent<TAggregateId>> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}