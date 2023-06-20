using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Impl;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticatedUser command)
        {
            return Json(await _userService.LoginAsync(command.Email, command.Password));
        }
    }
}
