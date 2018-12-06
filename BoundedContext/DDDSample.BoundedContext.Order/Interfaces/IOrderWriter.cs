using System.Threading.Tasks;

namespace DDDSample.BoundedContext.Order.Interfaces
{
    public interface IOrderWriter
    {
        Task CreateAsync(string customerId);

        Task AddProductAsync(string orderId, string productId, int quantity);

        Task ChangeProductQuantityAsync(string orderId, string productId, int quantity);
    }
}