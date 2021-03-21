using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Merchant;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IMerchantProductRepository
    {
        Task<MerchantProduct> GetAsync(Guid merchantCode, Guid merchantAddressCode, string sku);
        Task BulkCreateAsync(List<MerchantProduct> merchantProductList);
        Task<List<MerchantProduct>> GetBySearchAsync(Guid merchantCode, Guid merchantAddressCode, int limit, int skip, string search, string orderBy);
        Task UpdateAsync(string sku, Guid merchantCode, Guid merchantAddressCode, MerchantProduct merchantProduct);
        Task UpdateStockAsync(string sku, Guid merchantCode, Guid merchantAddressCode, decimal stock);
        Task CreateAsync(MerchantProduct newMerchantProduct);

        Task<bool> DeleteAsync(string sku, Guid merchantCode, Guid merchantAddressCode);
    }
}
