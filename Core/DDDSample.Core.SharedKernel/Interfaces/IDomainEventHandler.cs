using System.Threading.Tasks;

namespace DDDSample.Core.SharedKernel.Interfaces
{
    public interface IDomainEventHandler<TAggregateId, TEvent>
        where TEvent : IDomainEvent<TAggregateId>
    {
        Task HandleAsync(TEvent @event);
    }
}