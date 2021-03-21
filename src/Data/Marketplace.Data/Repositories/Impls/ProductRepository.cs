using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Product;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Marketplace.Data.Repositories.Impls
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public ProductRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task ActivateSkuAsync(string sku)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Sku, sku);
            var update = Builders<Product>.Update.Set(x => x.IsActive, true);
            await _mongoDbContext.Products.FindOneAndUpdateAsync(filter, update);
        }

        public async Task CreateAsync(Product product)
        {
            await _mongoDbContext.Products.InsertOneAsync(product);
        }

        public async Task DeactivateSkuAsync(string sku)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Sku, sku);
            var update = Builders<Product>.Update.Set(x => x.IsActive, false);
            await _mongoDbContext.Products.FindOneAndUpdateAsync(filter, update);
        }

        public async Task DeleteSkuAsync(string sku)
        {
            await _mongoDbContext.Products.DeleteOneAsync(x => x.Sku == sku);
        }

        public async Task<List<Product>> GetAllAsync(string search, int skip, int limit, string orderBy)
        {
            var filter = Builders<Product>.Filter.Text(search);
            var findOptions = new FindOptions<Product, Product>
            {
                Sort = orderBy.ToSortDefinition<Product>(),
                Skip = skip,
                Limit = limit
            };
            List<Product> products;
            if (string.IsNullOrWhiteSpace(search))
                products = (await _mongoDbContext.Products.FindAsync(new BsonDocument(), findOptions)).ToList();
            else
                products = (await _mongoDbContext.Products.FindAsync(filter, findOptions)).ToList();
            return products;
        }

        public async Task<ICollection<Product>> GetProductWithTagsAsync(List<string> tags)
        {
            var filter = Builders<Product>.Filter.ElemMatch(x => x.Tags, x => tags.Contains(x));
            var products = await _mongoDbContext.Products.FindAsync(filter);

            return products.ToList();
        }

        public async Task<Product> GetWithSkuAsync(string sku)
        {
            var product = await _mongoDbContext.Products.FindAsync(x => x.Sku == sku);

            return product.FirstOrDefault();
        }

        public async Task UpdateAsync(string sku, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Sku, sku);
            var update = Builders<Product>.Update.Set(x => x.IsActive, product.IsActive)
                .Set(x => x.Name, product.Name)
                //.Set(x => x.Images, product.Images)
                .Set(x => x.Tags, product.Tags)
                .Set(x => x.Unit, product.Unit)
                .Set(x => x.CreatedBy, product.CreatedBy)
                .Set(x => x.Barcode, product.Barcode)
                .Set(x => x.Description, product.Description);
            await _mongoDbContext.Products.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<List<Product>> GetAllWithBarcodeAsync(string barcode, int skip, int limit)
        {
            var filter = Builders<Product>.Filter.Text("{name : /.*{barcode}.*/}".Replace("barcode", barcode));
            var products = await _mongoDbContext.Products.Find(filter).Skip(skip).Limit(limit).ToListAsync();
            return products;
        }

        public async Task<bool> RemoveTags(string sku, List<string> tags)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Sku, sku);
            var update = Builders<Product>.Update.PullAll(x => x.Tags, tags);

            var response = await _mongoDbContext.Products.UpdateOneAsync(filter, update);

            return response.IsAcknowledged;
        }

        public async Task<bool> PushImageUrls(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Sku, product.Sku);
            var update = Builders<Product>.Update.PushEach(x => x.Images, product.Images);

            var response = await _mongoDbContext.Products.UpdateOneAsync(filter, update);

            return response.IsAcknowledged;
        }
    }
}
