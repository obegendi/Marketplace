using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Product;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IProductTagsRepository
    {
        Task CreateAsync(ProductTag productTag);
        Task CreateAsync(IEnumerable<ProductTag> productTags);
        Task<IEnumerable<ProductTag>> GetAllAsync();
    }
}
