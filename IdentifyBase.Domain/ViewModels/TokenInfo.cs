using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Domain.ViewModels
{
    public sealed record TokenInfo
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
