using DDDSample.BoundedContext.Order.Interfaces;
using DDDSample.BoundedContext.Orders.Events;
using DDDSample.BoundedContext.Orders.WriteModels;
using DDDSample.Core.Repository.WriteModels;
using DDDSample.Core.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDSample.Application.Services.Orders
{
    public class OrderWriter : IOrderWriter
    {
        private readonly IWritterRepository<Order, OrderId> orderRepository;
        private readonly ITransientDomainEventSubscriber subscriber;
        private readonly IEnumerable<IDomainEventHandler<OrderId, OrderCreated>> orderCreatedEventHandlers;
        private readonly IEnumerable<IDomainEventHandler<OrderId, ProductAdded>> productAddedEventHandlers;
        private readonly IEnumerable<IDomainEventHandler<OrderId, ProductQuantityChanged>> productQuantityChangedEventHandlers;

        public OrderWriter(IWritterRepository<Order, OrderId> orderRepository,
            ITransientDomainEventSubscriber subscriber,
            IEnumerable<IDomainEventHandler<OrderId, OrderCreated>> orderCreatedEventHandlers,
            IEnumerable<IDomainEventHandler<OrderId, ProductAdded>> productAddedEventHandlers,
            IEnumerable<IDomainEventHandler<OrderId, ProductQuantityChanged>> productQuantityChangedEventHandlers)
        {
            this.orderRepository = orderRepository;
            this.subscriber = subscriber;
            this.orderCreatedEventHandlers = orderCreatedEventHandlers;
            this.productAddedEventHandlers = productAddedEventHandlers;
            this.productQuantityChangedEventHandlers = productQuantityChangedEventHandlers;
        }

        public async Task AddProductAsync(string cartId, string productId, int quantity)
        {
            var order = await orderRepository.GetByIdAsync(new OrderId(cartId));

            subscriber.Subscribe<ProductAdded>(async @event => await HandleAsync(productAddedEventHandlers, @event));
            order.AddProduct(new ProductId(productId), quantity);
            await orderRepository.SaveAsync(order);
        }

        public async Task ChangeProductQuantityAsync(string cartId, string productId, int quantity)
        {
            var order = await orderRepository.GetByIdAsync(new OrderId(cartId));

            subscriber.Subscribe<ProductQuantityChanged>(async @event => await HandleAsync(productQuantityChangedEventHandlers, @event));
            order.ChangeProductQuantity(new ProductId(productId), quantity);
            await orderRepository.SaveAsync(order);
        }

        public async Task CreateAsync(string customerId)
        {
            var order = new Order(OrderId.NewOrderId(), new CustomerId(customerId));

            subscriber.Subscribe<OrderCreated>(async @event => await HandleAsync(orderCreatedEventHandlers, @event));
            await orderRepository.SaveAsync(order);
        }

        public async Task HandleAsync<T>(IEnumerable<IDomainEventHandler<OrderId, T>> handlers, T @event)
            where T : IDomainEvent<OrderId>
        {
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}