using IdentifyBase.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Application.Abstractions.Database
{
    public interface IMainContextRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : Entity
    {
    }
}
