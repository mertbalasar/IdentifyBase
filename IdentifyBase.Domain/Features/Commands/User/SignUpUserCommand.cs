using IdentifyBase.Domain.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Domain.Features.Commands.User
{
    public sealed class SignUpUserCommand : IRequest<HandlerResponse>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
