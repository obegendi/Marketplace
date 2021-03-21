using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Marketplace.Data.Repositories.Impls
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MerchantRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task AddAddressMetaDataAsync(string merchantName, string addressTitle)
        {
            var filter = Builders<Merchant>.Filter.Eq(x => x.Name, merchantName);
            var update = Builders<Merchant>.Update.Push(x => x.ActiveAddressNames, addressTitle);
            var result = await _mongoDbContext.Merchants.UpdateOneAsync(filter, update);
        }

        public async Task RemoveAddressMetaDataAsync(string merchantName, string addressTitle)
        {
            var filter = Builders<Merchant>.Filter.Eq(x => x.Name, merchantName);
            var update = Builders<Merchant>.Update.Pull(x => x.ActiveAddressNames, addressTitle);
            var result = await _mongoDbContext.Merchants.UpdateOneAsync(filter, update);
        }

        public async Task CreateAsync(Merchant merchant)
        {
            await _mongoDbContext.Merchants.InsertOneAsync(merchant);
        }

        public async Task<ICollection<Merchant>> GetAllAsync()
        {
            var merchants = await _mongoDbContext.Merchants.FindAsync(new BsonDocument());
            return merchants.ToList();
        }

        public async Task<Merchant> GetAsync(Guid merchantCode)
        {
            var merchants = await _mongoDbContext.Merchants.FindAsync(x => x.Code == merchantCode);
            return merchants.FirstOrDefault();
        }
    }
}
