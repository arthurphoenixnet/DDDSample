using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.BoundedContext.Orders.ReadModels
{
    public class OrderItem : IReadEntity
    {
        public string Id { get; private set; }

        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public static OrderItem CreateFor(string cartId, string productId)
        {
            return new OrderItem
            {
                Id = IdFor(cartId, productId),
                OrderId = cartId,
                ProductId = productId
            };
        }

        public static string IdFor(string cartId, string productId)
        {
            return $"{productId}@{cartId}";
        }
    }
}