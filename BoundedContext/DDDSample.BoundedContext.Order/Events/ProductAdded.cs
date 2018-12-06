using DDDSample.BoundedContext.Orders.WriteModels;
using DDDSample.Core.SharedKernel.BaseClasses;
using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.BoundedContext.Orders.Events
{
    public class ProductAdded : DomainEvent<OrderId>
    {
        #region Constructors

        private ProductAdded()
        {
        }

        internal ProductAdded(ProductId productId, int quantity) : base()
        {
            ProductId = productId;
            Quantity = quantity;
        }

        internal ProductAdded(OrderId aggregateId, long aggregateVersion, ProductId productId, int quantity) : base(aggregateId, aggregateVersion)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        #endregion Constructors

        #region Properties

        public ProductId ProductId { get; private set; }

        public int Quantity { get; private set; }

        #endregion Properties

        #region Override Methods

        public override IDomainEvent<OrderId> WithAggregate(OrderId aggregateId, long aggregateVersion)
        {
            return new ProductAdded(aggregateId, aggregateVersion, ProductId, Quantity);
        }

        #endregion Override Methods
    }
}