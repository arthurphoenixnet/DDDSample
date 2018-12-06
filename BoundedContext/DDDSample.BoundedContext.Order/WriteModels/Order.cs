using DDDSample.BoundedContext.Orders.Events;
using DDDSample.BoundedContext.Orders.Exceptions;
using DDDSample.Core.SharedKernel.BaseClasses;
using DDDSample.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDSample.BoundedContext.Orders.WriteModels
{
    public class Order : AggregateEntity<OrderId>
    {
        public const int ProductQuantityThreshold = 50;

        #region Constructors

        private Order()
        {
            Items = new List<OrderItem>();
            this._evtHandlerMap.Add(typeof(OrderCreated).FullName, ApplyOrderCreated);
            this._evtHandlerMap.Add(typeof(ProductAdded).FullName, ApplyProductAdded);
            this._evtHandlerMap.Add(typeof(ProductQuantityChanged).FullName, ApplyProductQuantityChanged);
        }

        public Order(OrderId cartId, CustomerId customerId) : this()
        {
            if (cartId == null)
            {
                throw new ArgumentNullException(nameof(cartId));
            }
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            RaiseEvent(new OrderCreated(cartId, customerId));
        }

        #endregion Constructors

        #region Properties

        private CustomerId CustomerId { get; set; }

        private List<OrderItem> Items { get; set; }

        #endregion Properties

        #region Public Methods

        public void AddProduct(ProductId productId, int quantity)
        {
            if (productId == null)
            {
                throw new ArgumentNullException(nameof(productId));
            }
            if (ContainsProduct(productId))
            {
                throw new OrderException($"Product {productId} already added");
            }
            CheckQuantity(productId, quantity);
            RaiseEvent(new ProductAdded(productId, quantity));
        }

        public void ChangeProductQuantity(ProductId productId, int quantity)
        {
            if (!ContainsProduct(productId))
            {
                throw new OrderException($"Product {productId} not found");
            }
            CheckQuantity(productId, quantity);
            RaiseEvent(new ProductQuantityChanged(productId, GetCartItemByProduct(productId).Quantity, quantity));
        }

        #endregion Public Methods

        #region Event Applying Methods

        internal void ApplyOrderCreated(IDomainEvent evt)
        {
            OrderCreated createdEvet = evt as OrderCreated;
            Id = createdEvet?.AggregateId;
            CustomerId = createdEvet?.CustomerId;
        }

        internal void ApplyProductAdded(IDomainEvent ev)
        {
            ProductAdded addedEvent = ev as ProductAdded;
            Items.Add(new OrderItem(addedEvent?.ProductId, addedEvent?.Quantity ?? 0));
        }

        internal void ApplyProductQuantityChanged(IDomainEvent ev)
        {
            ProductQuantityChanged pqChangedEvent = ev as ProductQuantityChanged;
            var existingItem = Items.Single(x => x.ProductId == pqChangedEvent?.ProductId);

            Items.Remove(existingItem);
            Items.Add(existingItem.WithQuantity(pqChangedEvent?.NewQuantity ?? 0));
        }

        #endregion Event Applying Methods

        #region Private Methods

        private bool ContainsProduct(ProductId productId)
        {
            return Items.Any(x => x.ProductId == productId);
        }

        private OrderItem GetCartItemByProduct(ProductId productId)
        {
            return Items.Single(x => x.ProductId == productId);
        }

        private static void CheckQuantity(ProductId productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            }
            if (quantity > ProductQuantityThreshold)
            {
                throw new OrderException($"Quantity for product {productId} must be less than or equal to {ProductQuantityThreshold}");
            }
        }

        #endregion Private Methods

        #region Override Methods

        public override string ToString()
        {
            return $"{{ Id: \"{Id}\", CustomerId: \"{CustomerId.IdAsString()}\", Items: [{string.Join(", ", Items.Select(x => x.ToString()))}] }}";
        }

        #endregion Override Methods
    }
}