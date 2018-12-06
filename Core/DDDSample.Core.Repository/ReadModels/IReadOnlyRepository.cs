using DDDSample.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDSample.Core.Repository.ReadModels
{
    public interface IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(string id);
    }
}