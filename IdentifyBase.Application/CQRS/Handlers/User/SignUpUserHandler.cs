using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Application.CQRS.Handlers.User
{
    public sealed class SignUpUserHandler : IRequestHandler<SignUpUserCommand, HandlerResponse>
    {
        private readonly IUserService _userService;

        public SignUpUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<HandlerResponse> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.SignUp(request);
        }
    }
}
