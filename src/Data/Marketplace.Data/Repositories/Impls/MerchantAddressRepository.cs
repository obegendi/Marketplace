using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using Marketplace.Domain.SharedKernel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Impls
{
    public class MerchantAddressRepository : IMerchantAddressRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MerchantAddressRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task AddAsync(MerchantAddress merchantLocation)
        {
            await _mongoDbContext.MerchantAddresses.InsertOneAsync(merchantLocation);
        }

        public async Task DeleteAsync(Guid merchantCode, Guid merchantAddressCode)
        {
            var filter = Builders<MerchantAddress>.Filter.Eq(x => x.Code, merchantAddressCode) &
                         Builders<MerchantAddress>.Filter.Eq(x => x.MerchantCode, merchantCode);
            await _mongoDbContext.MerchantAddresses.DeleteOneAsync(filter);
        }

        public async Task<long> PullAvailableLocationsAsync(Guid merchantCode, Guid merchantAddressCode, List<Location> requestLocations)
        {
            var filter = Builders<MerchantAddress>.Filter.Eq(x => x.MerchantCode, merchantCode) &
                         Builders<MerchantAddress>.Filter.Eq(x => x.Code, merchantAddressCode);
            var update = Builders<MerchantAddress>.Update.PullAll(x => x.AvailableLocations, requestLocations);

            var response = await _mongoDbContext.MerchantAddresses.UpdateOneAsync(filter, update);
            return response.MatchedCount;
        }

        public async Task<ICollection<MerchantAddress>> GetAllAsync(Guid merchantCode, string search, int skip, int limit, string orderBy)
        {
            var findOptions = new FindOptions<MerchantAddress, MerchantAddress>
            {
                Sort = orderBy.ToSortDefinition<MerchantAddress>(),
                Skip = skip,
                Limit = limit
            };

            ICollection<MerchantAddress> merchantAddress;
            if (search is null)
            {
                merchantAddress = (await _mongoDbContext.MerchantAddresses.FindAsync(new BsonDocument(), findOptions)).ToList();
            }
            else
            {
                var filter = Builders<MerchantAddress>.Filter.Text(search);
                merchantAddress = (await _mongoDbContext.MerchantAddresses.FindAsync(filter, findOptions)).ToList();
            }
            return merchantAddress;
        }

        public async Task<MerchantAddress> GetAsync(Guid merchantCode, Guid merchantAddressCode)
        {
            var merchantLocation = await _mongoDbContext.MerchantAddresses.FindAsync(x => x.MerchantCode == merchantCode && x.Code == merchantAddressCode);
            return merchantLocation.FirstOrDefault();
        }

        public async Task<MerchantAddress> GetAsync(Guid merchantCode, string name)
        {
            var merchantLocation = await _mongoDbContext.MerchantAddresses.FindAsync(x => x.MerchantCode == merchantCode && x.Name == name);
            return merchantLocation.FirstOrDefault();
        }

        public async Task UpdateAsync(MerchantAddress merchantAddress)
        {
            var filter = Builders<MerchantAddress>.Filter.Eq(x => x.Code, merchantAddress.Code) &
                         Builders<MerchantAddress>.Filter.Eq(x => x.MerchantCode, merchantAddress.MerchantCode);

            var update = Builders<MerchantAddress>.Update
                .Set(x => x.IsActive, merchantAddress.IsActive)
                .Set(x => x.Address, merchantAddress.Address)
                .Set(x => x.City, merchantAddress.City)
                .Set(x => x.Country, merchantAddress.Country)
                .Set(x => x.District, merchantAddress.District)
                .Set(x => x.ExtraInfo, merchantAddress.ExtraInfo)
                .Set(x => x.LastChangeTime, merchantAddress.LastChangeTime)
                .Set(x => x.Location, merchantAddress.Location)
                .Set(x => x.State, merchantAddress.State)
                .Set(x => x.Town, merchantAddress.Town)
                .Set(x => x.State, merchantAddress.State)
                .Set(x => x.WorkingHours, merchantAddress.WorkingHours)
                .Set(x => x.Name, merchantAddress.Name)
                .Set(x => x.AvailableLocations, merchantAddress.AvailableLocations);

            var result = await _mongoDbContext.MerchantAddresses.UpdateOneAsync(filter, update);
        }

        public async Task UpdateIsActiveAsync(string merchantName, string addressName, bool isActive)
        {
            var filter = Builders<MerchantAddress>.Filter.Eq(x => x.MerchantName, merchantName) &
                         Builders<MerchantAddress>.Filter.Eq(x => x.Name, addressName);

            var update = Builders<MerchantAddress>.Update.Set(x => x.IsActive, isActive);
            await _mongoDbContext.MerchantAddresses.UpdateOneAsync(filter, update);
        }
    }
}
