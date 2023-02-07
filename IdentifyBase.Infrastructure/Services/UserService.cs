using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Entities;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<HandlerResponse> SignUp(SignUpUserCommand command)
        {
            var response = new HandlerResponse();

            try
            {
                var createUser = await _userManager.CreateAsync(new()
                {
                    UserName = command.UserName,
                    Email = command.Email
                }, command.Password);

                if (createUser != null && createUser.Succeeded) return response;
                else
                {
                    response.Succeed = false;
                    response.Message = "Could not sign up to user";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Succeed = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
