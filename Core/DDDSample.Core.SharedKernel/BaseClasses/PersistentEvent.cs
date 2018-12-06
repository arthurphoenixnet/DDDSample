using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.Core.SharedKernel.BaseClasses
{
    public class PersistentEvent<TAggregateId>
    {
        public PersistentEvent(IDomainEvent<TAggregateId> domainEvent, long eventNumber)
        {
            DomainEvent = domainEvent;
            EventNumber = eventNumber;
        }

        public long EventNumber { get; }

        public IDomainEvent<TAggregateId> DomainEvent { get; }
    }
}