using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.BoundedContext.Orders.ReadModels
{
    public class Order : IReadEntity
    {
        public string Id { get; set; }

        public int TotalItems { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}