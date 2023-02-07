using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using IdentifyBase.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<HandlerResponse> SignUp(SignUpUserCommand command);
        Task<HandlerResponse<TokenInfo>> SignIn(SignInUserCommand command);
    }
}
