using DDDSample.Core.SharedKernel.BaseClasses;
using System.Collections.Generic;

namespace DDDSample.BoundedContext.Orders.WriteModels
{
    public class OrderItem : ValueObject
    {
        #region Constructors

        public OrderItem(ProductId prdId, int quantity)
        {
            this.ProductId = prdId;
            this.Quantity = quantity;
        }

        #endregion Constructors

        #region Properties

        public ProductId ProductId { get; }

        public int Quantity { get; }

        #endregion Properties

        #region Public Methods

        public OrderItem WithQuantity(int quantity)
        {
            return new OrderItem(this.ProductId, quantity);
        }

        #endregion Public Methods

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.ProductId;
            yield return this.Quantity;
        }
    }
}