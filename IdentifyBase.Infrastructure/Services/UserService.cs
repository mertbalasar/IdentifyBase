using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Entities;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using IdentifyBase.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace IdentifyBase.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<HandlerResponse> SignUp(SignUpUserCommand command)
        {
            var response = new HandlerResponse();

            try
            {
                User user = new()
                {
                    UserName = command.UserName,
                    Email = command.Email
                };

                IdentityResult? createUser = await _userManager.CreateAsync(user, command.Password);

                if (createUser == null || (createUser != null && !createUser.Succeeded)) 
                {
                    response.Succeed = false;
                    response.Message = "Could not create an user (" + string.Join(", ", createUser.Errors.ToList().Select(x => x.Description).ToArray()) + ")";
                    return response;
                }

                var roleExist = await _roleManager.RoleExistsAsync("standart");
                IdentityResult? roleCreate = null;
                IdentityResult? userRole = null;

                if (!roleExist)
                {
                    roleCreate = await _roleManager.CreateAsync(new Role()
                    {
                        Name = "standart"
                    });
                }

                if (roleCreate == null || (roleCreate != null && !roleCreate.Succeeded))
                {
                    response.Succeed = false;
                    response.Message = "Could not create a role (" + string.Join(", ", roleCreate.Errors.ToList().Select(x => x.Description).ToArray()) + ")";
                    return response;
                }

                userRole = await _userManager.AddToRoleAsync(user, "standart");

                if (userRole == null || (userRole != null && !userRole.Succeeded))
                {
                    response.Succeed = false;
                    response.Message = "Could not append role to created user (" + string.Join(", ", userRole.Errors.ToList().Select(x => x.Description).ToArray()) + ")";
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Succeed = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<HandlerResponse<TokenInfo>> SignIn(SignInUserCommand command)
        {
            var response = new HandlerResponse<TokenInfo>();

            try
            {
                var userExist = await _userManager.FindByNameAsync(command.UserName);

                if (userExist == null)
                {
                    response.Succeed = false;
                    response.Message = "Wrong username";
                    return response;
                }

                var checkPassword = await _userManager.CheckPasswordAsync(userExist, command.Password);

                if (!checkPassword)
                {
                    response.Succeed = false;
                    response.Message = "Wrong password";
                    return response;
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userExist.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userExist.Id),
                    new Claim(ClaimTypes.Role, "standart")
                };

                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

                var token = new JwtSecurityToken(
                        issuer: "http://google.com",
                        audience: "http://google.com",
                        expires: DateTime.UtcNow.AddHours(1),
                        claims: claims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                        );

                response.Result = new()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireAt = token.ValidTo
                };

                return response;
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
