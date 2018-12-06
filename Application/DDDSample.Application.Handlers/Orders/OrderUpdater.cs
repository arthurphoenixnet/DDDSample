using DDDSample.BoundedContext.Orders.Events;
using DDDSample.BoundedContext.Orders.ReadModels;
using DDDSample.BoundedContext.Orders.WriteModels;
using DDDSample.Core.Repository.ReadModels;
using DDDSample.Core.SharedKernel.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using OrderItemReadModel = DDDSample.BoundedContext.Orders.ReadModels.OrderItem;
using OrderReadModel = DDDSample.BoundedContext.Orders.ReadModels.Order;

namespace DDDSample.Application.Handlers.Orders
{
    public class CartUpdater : IDomainEventHandler<OrderId, OrderCreated>,
        IDomainEventHandler<OrderId, ProductAdded>,
        IDomainEventHandler<OrderId, ProductQuantityChanged>
    {
        private readonly IReadOnlyRepository<Customer> customerRepository;
        private readonly IReadOnlyRepository<Product> productRepository;
        private readonly IReaderRepository<OrderReadModel> orderRepository;
        private readonly IReaderRepository<OrderItemReadModel> orderItemRepository;

        public CartUpdater(IReadOnlyRepository<Customer> customerRepository,
            IReadOnlyRepository<Product> productRepository, IReaderRepository<OrderReadModel> orderRepository,
            IReaderRepository<OrderItemReadModel> orderItemRepository)
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            var customer = await customerRepository.GetByIdAsync(@event.CustomerId.IdAsString());

            await orderRepository.InsertAsync(new OrderReadModel
            {
                Id = @event.AggregateId.IdAsString(),
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                TotalItems = 0
            });
        }

        public async Task HandleAsync(ProductAdded @event)
        {
            var product = await productRepository.GetByIdAsync(@event.ProductId.IdAsString());
            var cart = await orderRepository.GetByIdAsync(@event.AggregateId.IdAsString());
            var cartItem = OrderItemReadModel.CreateFor(@event.AggregateId.IdAsString(), @event.ProductId.IdAsString());

            cartItem.ProductName = product.Name;
            cartItem.Quantity = @event.Quantity;
            cart.TotalItems += @event.Quantity;
            await orderRepository.UpdateAsync(cart);
            await orderItemRepository.InsertAsync(cartItem);
        }

        public async Task HandleAsync(ProductQuantityChanged @event)
        {
            var cartItemId = OrderItemReadModel.IdFor(@event.AggregateId.IdAsString(), @event.ProductId.IdAsString());
            var cartItem = (await orderItemRepository
                .FindAllAsync(x => x.Id == cartItemId))
                .Single();
            var cart = await orderRepository.GetByIdAsync(@event.AggregateId.IdAsString());

            cart.TotalItems += @event.NewQuantity - @event.OldQuantity;
            cartItem.Quantity = @event.NewQuantity;

            await orderRepository.UpdateAsync(cart);
            await orderItemRepository.UpdateAsync(cartItem);
        }
    }
}