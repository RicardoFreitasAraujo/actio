using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Services
{
    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            _webHost = webHost;
            _bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            using (var serviceScope = _webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var handler = (ICommandHandler<TCommand>)serviceScope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));
                _bus.WithCommandHandlerAsync(handler);

                return this;
            }
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            using (var serviceScope = _webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var handler = (IEventHandler<TEvent>)serviceScope.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));

                _bus.WithEventHandlerAsync(handler);

                return this;
            }
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }

}
