using DDDSample.Core.SharedKernel.BaseClasses;
using DDDSample.Core.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDSample.Core.Repository.WriteModels
{
    public interface IEventStore
    {
        Task<IEnumerable<PersistentEvent<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
            where TAggregateId : IAggregateId;

        Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
            where TAggregateId : IAggregateId;
    }
}