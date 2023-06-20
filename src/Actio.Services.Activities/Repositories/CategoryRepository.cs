using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private  IMongoDatabase _database { get; set; }

        public CategoryRepository(IMongoDatabase dabase)
        {
            _database = dabase;
        }

        public async Task<Category> GetAsync(string name)
        {
            return await this.Collection.AsQueryable().FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());
        }

        public async Task AddAsync(Category category)
        {
            await this.Collection.InsertOneAsync(category);
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
        {
            return await this.Collection.AsQueryable().ToListAsync();
        }

        private IMongoCollection<Category> Collection
            => _database.GetCollection<Category>("Categories");
        
        
    }
}
