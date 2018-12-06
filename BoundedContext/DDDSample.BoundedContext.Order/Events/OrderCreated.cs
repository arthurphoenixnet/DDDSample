using DDDSample.BoundedContext.Orders.WriteModels;
using DDDSample.Core.SharedKernel.BaseClasses;
using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.BoundedContext.Orders.Events
{
    public class OrderCreated : DomainEvent<OrderId>
    {
        #region Constructors

        private OrderCreated()
        {
        }

        internal OrderCreated(OrderId aggregateId, CustomerId customerId) : base(aggregateId)
        {
            CustomerId = customerId;
        }

        private OrderCreated(OrderId aggregateId, long aggregateVersion, CustomerId customerId) : base(aggregateId, aggregateVersion)
        {
            CustomerId = customerId;
        }

        #endregion Constructors

        #region Properties

        public CustomerId CustomerId { get; private set; }

        #endregion Properties

        #region Override Methods

        public override IDomainEvent<OrderId> WithAggregate(OrderId aggregateId, long aggregateVersion)
        {
            return new OrderCreated(aggregateId, aggregateVersion, CustomerId);
        }

        #endregion Override Methods
    }
}