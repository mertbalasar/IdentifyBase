using IdentifyBase.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentifyBase.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetUser()
        {
            var response = await _userManager.CreateAsync(new Domain.Entities.User() { UserName = "mertbalsr", Email = "mertblsr@gmail.com" }, "password");
            return Ok();
        }
    }
}
