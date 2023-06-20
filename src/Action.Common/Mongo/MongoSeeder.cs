using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase Dabase;

        public MongoSeeder(IMongoDatabase dabase)
        {
            Dabase = dabase;
        }

        
        public async Task SeedAsync()
        {
            var collectionCursos = await Dabase.ListCollectionsAsync();
            var collection = await collectionCursos.ToListAsync();
            if (collection.Any())
            {
                return;
            }
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
