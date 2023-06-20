using Actio.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity model)
        {
            await Collection.InsertOneAsync(model);
        }

        public async Task<IEnumerable<Activity>> BrowserAsync(Guid userId)
        {
            return await Collection
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");
    }
}
