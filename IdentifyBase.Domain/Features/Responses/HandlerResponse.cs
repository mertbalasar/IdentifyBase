using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Domain.Features.Responses
{
    public class HandlerResponse
    {
        public bool Succeed { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

    public class HandlerResponse<T> : HandlerResponse
    {
        public T? Result { get; set; }
    }
}
