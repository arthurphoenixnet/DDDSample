using DDDSample.BoundedContext.Orders.Interfaces;
using DDDSample.BoundedContext.Orders.ReadModels;
using DDDSample.Core.Repository.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDSample.Application.Services.Orders
{
    public class CartReader : IOrderReader
    {
        private readonly IReadOnlyRepository<Order> orderRepository;
        private readonly IReadOnlyRepository<OrderItem> orderItemRepository;

        public CartReader(IReadOnlyRepository<Order> orderRepository, IReadOnlyRepository<OrderItem> orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<Order>> FindAllAsync(Expression<Func<Order, bool>> predicate)
        {
            return await orderRepository.FindAllAsync(predicate);
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            return await orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetItemsOfAsync(string orderId)
        {
            return await orderItemRepository.FindAllAsync(x => x.OrderId == orderId);
        }
    }
}