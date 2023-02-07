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
    public interface IReadRepository<T> where T : Entity
    {
        Task<RepositoryResponse<T>> GetByIdAsync(Guid id);
        Task<RepositoryResponse<IEnumerable<T>>> GetAllAsync();
        Task<RepositoryResponse<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> expression);
    }
}
