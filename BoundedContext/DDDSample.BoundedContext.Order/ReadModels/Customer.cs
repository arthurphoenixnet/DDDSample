using DDDSample.Core.SharedKernel.Interfaces;

namespace DDDSample.BoundedContext.Orders.ReadModels
{
    public class Customer : IReadEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}