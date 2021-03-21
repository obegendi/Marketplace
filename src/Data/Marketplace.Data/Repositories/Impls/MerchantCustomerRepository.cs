using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant.Customer;
using Marketplace.Domain.SharedKernel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Impls
{
    public class MerchantCustomerRepository : IMerchantCustomerRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MerchantCustomerRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        public async Task InsertAsync(MerchantCustomer merchantCustomer)
        {
            await _mongoDbContext.MerchantCustomers.InsertOneAsync(merchantCustomer);
        }

        public async Task<MerchantCustomer> GetAsync(Guid code)
        {
            var filter = Builders<MerchantCustomer>.Filter.Eq(x => x.Code, code);
            var result = await _mongoDbContext.MerchantCustomers.FindAsync(filter);

            return result.FirstOrDefault();
        }

        public async Task<MerchantCustomer> UpdateAsync(MerchantCustomer customer)
        {
            var filter = Builders<MerchantCustomer>.Filter.Eq(x => x.Code, customer.Code);
            var update = Builders<MerchantCustomer>.Update.Set(x => x.Addresses, customer.Addresses)
                .Set(x => x.UpdateBy, customer.UpdateBy)
                .Set(x => x.Email, customer.Email)
                .Set(x => x.FirstName, customer.FirstName)
                .Set(x => x.UpdateDate, customer.UpdateDate)
                .Set(x => x.Phone, customer.Phone)
                .Set(x => x.Email, customer.Email)
                .Set(x => x.LastName, customer.LastName)
                .Set(x => x.Addresses, customer.Addresses);
            var result = await _mongoDbContext.MerchantCustomers.FindOneAndUpdateAsync(filter, update);

            return result;
        }

        public async Task<bool> PullAddressAsync(Guid code, Address address)
        {
            var filter = Builders<MerchantCustomer>.Filter.Eq(x => x.Code, code);

            var update = Builders<MerchantCustomer>.Update.Pull(x => x.Addresses, address);
            var result = await _mongoDbContext.MerchantCustomers.UpdateOneAsync(filter, update);
            if (result.IsAcknowledged)
                if (result.ModifiedCount > 0)
                    return true;
            return false;
        }

        public async Task<List<MerchantCustomer>> GetAllAsync(Guid merchantCode, string search, int skip, int limit, string orderBy)
        {
            var findOptions = new FindOptions<MerchantCustomer, MerchantCustomer>
            {
                Sort = orderBy.ToSortDefinition<MerchantCustomer>(),
                Skip = skip,
                Limit = limit
            };

            ICollection<MerchantCustomer> merchantCustomers;
            if (search is null)
            {
                merchantCustomers = (await _mongoDbContext.MerchantCustomers.FindAsync(new BsonDocument(), findOptions)).ToList();
            }
            else
            {
                var filter = Builders<MerchantCustomer>.Filter.Text(search);
                merchantCustomers = (await _mongoDbContext.MerchantCustomers.FindAsync(filter, findOptions)).ToList();
            }
            return merchantCustomers.ToList();
        }

        public async Task<bool> DeleteAsync(Guid code)
        {
            var result = await _mongoDbContext.MerchantCustomers.DeleteOneAsync(x => x.Code == code);
            if (result.IsAcknowledged)
                if (result.DeletedCount > 0)
                    return true;
            return false;
        }
    }
}
