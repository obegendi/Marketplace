using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Merchant;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IMerchantRepository
    {
        Task CreateAsync(Merchant merchant);
        Task<Merchant> GetAsync(Guid merchantCode);
        Task<ICollection<Merchant>> GetAllAsync();
        Task AddAddressMetaDataAsync(string merchantName, string addressTitle);
        Task RemoveAddressMetaDataAsync(string merchantName, string addressTitle);
    }
}
