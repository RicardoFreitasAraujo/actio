using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {

        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public CreateUserHandler(IBusClient busClient, IUserService userService /*, ILogger logger*/)
        {
            _busClient = busClient;
            _userService = userService;
            //_logger = new logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            //_logger.LogInformation($"Creating user: {command.Email}");

            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                return;
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Code, ex.Message));
                //_logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
        }
    }
}
