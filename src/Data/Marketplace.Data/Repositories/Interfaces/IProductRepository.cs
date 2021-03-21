using Marketplace.Domain.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product productEntity);
        Task<ICollection<Product>> GetProductWithTagsAsync(List<string> tags);
        Task ActivateSkuAsync(string sku);
        Task<Product> GetWithSkuAsync(string sku);
        Task UpdateAsync(string sku, Product product);
        Task DeactivateSkuAsync(string sku);
        Task DeleteSkuAsync(string sku);
        Task<List<Product>> GetAllAsync(string search, int skip, int limit, string orderBy);
        Task<bool> RemoveTags(string sku, List<string> tags);
        Task<bool> PushImageUrls(Product product);
    }
}
