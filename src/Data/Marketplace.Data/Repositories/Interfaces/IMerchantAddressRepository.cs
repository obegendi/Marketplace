using Marketplace.Domain.Merchant;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IMerchantAddressRepository
    {
        Task UpdateAsync(MerchantAddress merchantLocation);
        Task<ICollection<MerchantAddress>> GetAllAsync(Guid merchantCode, string search, int skip, int limit, string orderBy);
        Task<MerchantAddress> GetAsync(Guid merchantCode, Guid merchantAddressCode);
        Task<MerchantAddress> GetAsync(Guid merchantCode, string name);
        Task AddAsync(MerchantAddress merchantLocation);
        Task UpdateIsActiveAsync(string merchantName, string addressName, bool isActive);
        Task DeleteAsync(Guid merchantCode, Guid merchantAddressCode);
        Task<long> PullAvailableLocationsAsync(Guid merchantCode, Guid merchantAddressCode, List<Location> requestLocations);
    }
}
