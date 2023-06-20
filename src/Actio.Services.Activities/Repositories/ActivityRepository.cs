using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private IMongoDatabase _database { get; set; }

        public ActivityRepository(IMongoDatabase dabase)
        {
            _database = dabase;
        }

        public async Task AddAsync(Activity activity)
        {
            await this.Collection.InsertOneAsync(activity);
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await this.Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");
    }
}
