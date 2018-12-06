using DDDSample.Core.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace DDDSample.Core.Repository.WriteModels
{
    public interface IWritterRepository<TAggregate, TAggregateId>
        where TAggregate : IAggregate<TAggregateId>
    {
        Task<TAggregate> GetByIdAsync(TAggregateId id);

        Task SaveAsync(TAggregate aggregate);
    }
}