using DDDSample.BoundedContext.Orders.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDSample.BoundedContext.Orders.Interfaces
{
    public interface IOrderReader
    {
        Task<ReadModels.Order> GetByIdAsync(string id);

        Task<IEnumerable<ReadModels.Order>> FindAllAsync(Expression<Func<ReadModels.Order, bool>> predicate);

        Task<IEnumerable<OrderItem>> GetItemsOfAsync(string orderId);
    }
}