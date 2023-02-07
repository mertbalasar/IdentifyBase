using IdentifyBase.Domain.Commons;
using IdentifyBase.Domain.Features.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Application.Abstractions.Database
{
    public interface IWriteRepository<T> where T : Entity
    {
        Task<RepositoryResponse> AddAsync(T entity);
        Task<RepositoryResponse> AddRangeAsync(IEnumerable<T> entities);
        RepositoryResponse Remove(T entity);
        RepositoryResponse RemoveRange(IEnumerable<T> entities);
        RepositoryResponse Update(T entity);
        RepositoryResponse UpdateRange(IEnumerable<T> entities);
        Task<RepositoryResponse> RemoveBulkAsync(Expression<Func<T, bool>> expression);
        Task<RepositoryResponse> UpdateBulkAsync<TDestination>(Expression<Func<T, bool>> expression, Func<T, TDestination> field, Func<T, TDestination> value);
    }
}
