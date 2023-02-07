using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using IdentifyBase.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Application.CQRS.Handlers.User
{
    public sealed class SignInUserHandler : IRequestHandler<SignInUserCommand, HandlerResponse<TokenInfo>>
    {
        private readonly IUserService _userService;

        public SignInUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<HandlerResponse<TokenInfo>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.SignIn(request);
        }
    }
}
