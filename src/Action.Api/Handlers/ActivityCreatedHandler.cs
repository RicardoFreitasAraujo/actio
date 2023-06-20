using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;
using System;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await this._activityRepository.AddAsync(new Activity { 
                Id = @event.Id,
                UserId = @event.UserId,
                Name = @event.Name,
            });
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
