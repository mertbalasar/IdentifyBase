using IdentifyBase.Domain.Entities;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using IdentifyBase.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentifyBase.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserCommand signUpUserCommand)
        {
            HandlerResponse response = await _mediator.Send(signUpUserCommand);

            if (response.Succeed)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand signInUserCommand)
        {
            HandlerResponse<TokenInfo> response = await _mediator.Send(signInUserCommand);

            if (response.Succeed)
            {
                return Ok(response.Result);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
