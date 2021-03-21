using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Merchant;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IMerchantUserRepository
    {
        Task<MerchantUser> GetAsync(string phone);
        Task<MerchantUser> GetAsync(string phone, string password);
        Task<MerchantUser> GetWithEmailAsync(string email);
        Task<MerchantUser> GetWithPhoneAsync(string phone);
        Task<bool> DeleteAsync(Guid merchantCode, Guid code);
        Task CreateAsync(MerchantUser user);
        Task<List<MerchantUser>> GetAllAsync(Guid merchantCode, string search, int limit, int skip, string orderBy);
        Task UpdatePasswordAsync(MerchantUser account);
        Task<MerchantUser> GetWithPhoneAsync(string phone, string password);
        Task<MerchantUser> GetWithEmailAsync(string email, string password);
        Task<bool> UpdateAsync(string phone, string updatePhone, string email, string firstName, string lastName, string password, bool isActive,
            List<string> claims);
    }
}
