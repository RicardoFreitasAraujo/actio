using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busclient;

        public ActivitiesController(IBusClient busclient)
        {
            _busclient = busclient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            await _busclient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get() => Content("Secured");
    }
}
