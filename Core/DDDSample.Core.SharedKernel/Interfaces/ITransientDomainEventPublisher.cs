using System.Threading.Tasks;

namespace DDDSample.Core.SharedKernel.Interfaces
{
    public interface ITransientDomainEventPublisher
    {
        Task PublishAsync<T>(T publishedEvent);
    }
}