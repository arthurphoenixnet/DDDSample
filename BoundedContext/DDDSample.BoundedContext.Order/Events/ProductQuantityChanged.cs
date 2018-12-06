using DDDSample.BoundedContext.Orders.WriteModels;
using DDDSample.Core.SharedKernel.BaseClasses;
using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.BoundedContext.Orders.Events
{
    public class ProductQuantityChanged : DomainEvent<OrderId>
    {
        #region Constructors

        private ProductQuantityChanged()
        {
        }

        internal ProductQuantityChanged(ProductId productId, int oldQuantity, int newQuantity) : base()
        {
            ProductId = productId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }

        private ProductQuantityChanged(OrderId aggregateId, long aggregateVersion, ProductId productId,
            int oldQuantity, int newQuantity) : base(aggregateId, aggregateVersion)
        {
            ProductId = productId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }

        #endregion Constructors

        #region Properties

        public ProductId ProductId { get; private set; }

        public int OldQuantity { get; private set; }

        public int NewQuantity { get; private set; }

        #endregion Properties

        #region Override Methods

        public override IDomainEvent<OrderId> WithAggregate(OrderId aggregateId, long aggregateVersion)
        {
            return new ProductQuantityChanged(aggregateId, aggregateVersion, ProductId, OldQuantity, NewQuantity);
        }

        #endregion Override Methods
    }
}