using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Impls
{
    public class MerchantUserRepository : IMerchantUserRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MerchantUserRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<MerchantUser> GetAsync(string phone, string password)
        {
            var users = await _mongoDbContext.MerchantUsers.FindAsync(x => x.IsActive && x.Phone == phone && x.Password == password);

            return users.FirstOrDefault();
        }

        public async Task<MerchantUser> GetAsync(string phone)
        {
            var users = await _mongoDbContext.MerchantUsers.FindAsync(x => x.Phone == phone);

            return users.FirstOrDefault();
        }

        public async Task<bool> DeleteAsync(Guid merchantCode, Guid code)
        {
            var filter = Builders<MerchantUser>.Filter.Eq(x => x.MerchantCode, merchantCode) & Builders<MerchantUser>.Filter.Eq(x => x.Code, code);
            var result = await _mongoDbContext.MerchantUsers.DeleteOneAsync(filter);

            if (result.IsAcknowledged)
                if (result.DeletedCount > 0)
                    return true;
            return false;
        }

        public async Task CreateAsync(MerchantUser user)
        {
            await _mongoDbContext.MerchantUsers.InsertOneAsync(user);
        }

        public async Task<List<MerchantUser>> GetAllAsync(Guid merchantCode, string search, int limit, int skip, string orderBy)
        {
            var users = new List<MerchantUser>();
            if (search is null)
            {
                users = (await _mongoDbContext.MerchantUsers.FindAsync(new BsonDocument(), new FindOptions<MerchantUser, MerchantUser>
                {
                    Sort = orderBy.ToSortDefinition<MerchantUser>("LastName"),
                    Skip = skip,
                    Limit = limit
                })).ToList();
            }
            else
            {
                var filter = Builders<MerchantUser>.Filter.Text(search) & Builders<MerchantUser>.Filter.Eq(x => x.MerchantCode, merchantCode);
                users = (await _mongoDbContext.MerchantUsers.FindAsync(filter, new FindOptions<MerchantUser, MerchantUser>
                {
                    Sort = orderBy.ToSortDefinition<MerchantUser>("LastName"),
                    Skip = skip,
                    Limit = limit
                })).ToList();
            }

            return users;
        }

        public async Task UpdatePasswordAsync(MerchantUser account)
        {
            var filter = Builders<MerchantUser>.Filter.Eq(x => x.Phone, account.Phone);
            var update = Builders<MerchantUser>.Update.Set(x => x.Password, account.Password);
            var result = await _mongoDbContext.MerchantUsers.UpdateOneAsync(filter, update);
        }

        public async Task<MerchantUser> GetWithPhoneAsync(string emailOrPhone, string password)
        {
            var user = await _mongoDbContext.MerchantUsers.FindAsync(x => x.IsActive && x.Phone == emailOrPhone && x.Password == password);
            return user.FirstOrDefault();
        }

        public async Task<MerchantUser> GetWithEmailAsync(string emailOrPhone, string password)
        {
            var user = await _mongoDbContext.MerchantUsers.FindAsync(x => x.IsActive && x.Email == emailOrPhone && x.Password == password);
            return user.FirstOrDefault();
        }

        public async Task<MerchantUser> GetWithEmailAsync(string email)
        {
            var user = await _mongoDbContext.MerchantUsers.FindAsync(x => x.IsActive && x.Email == email);
            return user.FirstOrDefault();
        }

        public async Task<MerchantUser> GetWithPhoneAsync(string phone)
        {
            var user = await _mongoDbContext.MerchantUsers.FindAsync(x => x.IsActive && x.Phone == phone);
            return user.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(string phone, string updatePhone, string email, string firstName, string lastName, string password, bool isActive,
            List<string> claims)
        {
            var filter = Builders<MerchantUser>.Filter.Eq(x => x.Phone, phone);
            var update = Builders<MerchantUser>.Update
                .Set(x => x.Phone, updatePhone)
                .Set(x => x.Email, email)
                .Set(x => x.FirstName, firstName)
                .Set(x => x.LastName, lastName)
                .Set(x => x.Password, password)
                .Set(x => x.IsActive, isActive)
                .Set(x => x.Claims, claims);
            var response = await _mongoDbContext.MerchantUsers.UpdateOneAsync(filter, update);
            if (response.IsAcknowledged)
                if (response.ModifiedCount > 0)
                    return true;
            return false;
        }
    }
}
