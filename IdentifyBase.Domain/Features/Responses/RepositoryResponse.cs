using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Domain.Features.Responses
{
    public class RepositoryResponse
    {
        public bool Succeed { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

    public class RepositoryResponse<T> : RepositoryResponse
    {
        public T? Result { get; set; }
    }
}
