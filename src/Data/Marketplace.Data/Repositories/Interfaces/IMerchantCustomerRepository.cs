using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Merchant.Customer;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IMerchantCustomerRepository
    {
        Task InsertAsync(MerchantCustomer customerEntity);
        Task<MerchantCustomer> GetAsync(Guid code);
        Task<MerchantCustomer> UpdateAsync(MerchantCustomer customer);
        Task<bool> PullAddressAsync(Guid code, Address address);
        Task<List<MerchantCustomer>> GetAllAsync(Guid merchantCode, string search, int skip, int limit, string orderBy);
        Task<bool> DeleteAsync(Guid code);
    }
}
