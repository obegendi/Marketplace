using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Product;
using MongoDB.Driver;

namespace Marketplace.Data.Repositories.Impls
{
    public class ProductTagRepository : IProductTagsRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public ProductTagRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task CreateAsync(ProductTag productTag)
        {
            await _mongoDbContext.ProductTags.InsertOneAsync(productTag);
        }

        public async Task CreateAsync(IEnumerable<ProductTag> productTags)
        {
            await _mongoDbContext.ProductTags.InsertManyAsync(productTags);
        }

        public async Task<IEnumerable<ProductTag>> GetAllAsync()
        {
            var tags = await _mongoDbContext.ProductTags.FindAsync(x => x.Name != string.Empty);
            return tags.ToList();
        }
    }
}
