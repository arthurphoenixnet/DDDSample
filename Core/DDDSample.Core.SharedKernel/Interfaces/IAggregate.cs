namespace DDDSample.Core.SharedKernel.Interfaces
{
    public interface IAggregate<TId>
    {
        TId Id { get; }
    }
}