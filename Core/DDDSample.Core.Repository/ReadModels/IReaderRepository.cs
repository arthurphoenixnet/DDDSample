using DDDSample.Core.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace DDDSample.Core.Repository.ReadModels
{
    public interface IReaderRepository<T> : IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}