using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Impls
{
    public class MerchantProductRepository : IMerchantProductRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MerchantProductRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        public async Task BulkCreateAsync(List<MerchantProduct> merchantProductList)
        {
            await _mongoDbContext.MerchantProducts.InsertManyAsync(merchantProductList);
        }

        public async Task UpdateAsync(string sku, Guid merchantCode, Guid merchantAddressCode, MerchantProduct merchantProduct)
        {
            var filter = Builders<MerchantProduct>.Filter.Eq(x => x.Sku, sku) &
                         Builders<MerchantProduct>.Filter.Eq(x => x.MerchantCode, merchantCode) &
                         Builders<MerchantProduct>.Filter.Eq(x => x.MerchantAddressCode, merchantAddressCode);

            var update = Builders<MerchantProduct>.Update.Set(x => x.IsActive, merchantProduct.IsActive)
                .Set(x => x.IsInfiniteStock, merchantProduct.IsInfiniteStock)
                .Set(x => x.Stock, merchantProduct.Stock)
                .Set(x => x.Price, merchantProduct.Price)
                .Set(x => x.PriceWithVat, merchantProduct.PriceWithVat)
                .Set(x => x.Vat, merchantProduct.Vat)
                .Set(x => x.Name, merchantProduct.Name);

            await _mongoDbContext.MerchantProducts.FindOneAndUpdateAsync(filter, update);

        }

        public async Task<MerchantProduct> GetAsync(Guid merchantCode, Guid merchantAddressCode, string sku)
        {
            var merchantProducts = await _mongoDbContext.MerchantProducts.FindAsync(x => x.MerchantAddressCode == merchantAddressCode && x.Sku == sku && x.MerchantCode == merchantCode);

            return merchantProducts.FirstOrDefault();
        }

        public async Task<List<MerchantProduct>> GetBySearchAsync(Guid merchantCode, Guid merchantAddressCode, int limit, int skip, string search, string orderBy)
        {
            var merchantProducts = new List<MerchantProduct>();
            var filter = Builders<MerchantProduct>.Filter.Eq(x => x.MerchantCode, merchantCode);
            var findOptions = new FindOptions<MerchantProduct, MerchantProduct>
            {
                Sort = orderBy.ToSortDefinition<MerchantProduct>("Name"),
                Skip = skip,
                Limit = limit
            };
            if (merchantAddressCode != Guid.Empty)
            {
                filter &= Builders<MerchantProduct>.Filter.Eq(x => x.MerchantAddressCode, merchantAddressCode);
            }
            if (string.IsNullOrWhiteSpace(search))
            {
                merchantProducts = (await _mongoDbContext.MerchantProducts.FindAsync(filter, findOptions)).ToList();
            }
            else
            {
                filter &= Builders<MerchantProduct>.Filter.Text(search);
                merchantProducts = (await _mongoDbContext.MerchantProducts.FindAsync(filter, findOptions)).ToList();
            }
            return merchantProducts;
        }

        public async Task CreateAsync(MerchantProduct newMerchantProduct)
        {
            await _mongoDbContext.MerchantProducts.InsertOneAsync(newMerchantProduct);
        }

        public async Task UpdateStockAsync(string sku, Guid merchantCode, Guid merchantAddressCode, decimal stock)
        {
            var filter = Builders<MerchantProduct>.Filter.Eq(x => x.Sku, sku) &
                         Builders<MerchantProduct>.Filter.Eq(x => x.MerchantCode, merchantCode) &
                         Builders<MerchantProduct>.Filter.Eq(x => x.MerchantAddressCode, merchantAddressCode);

            var update = Builders<MerchantProduct>.Update.Set(x => x.Stock, stock);

            await _mongoDbContext.MerchantProducts.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<bool> DeleteAsync(string sku, Guid merchantCode, Guid merchantAddressCode)
        {
            var filter = Builders<MerchantProduct>.Filter.Eq(x => x.MerchantCode, merchantCode) &
                         Builders<MerchantProduct>.Filter.Eq(x => x.MerchantAddressCode, merchantAddressCode) &
                         Builders<MerchantProduct>.Filter.Eq(x => x.Sku, sku);
            var result = await _mongoDbContext.MerchantProducts.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
