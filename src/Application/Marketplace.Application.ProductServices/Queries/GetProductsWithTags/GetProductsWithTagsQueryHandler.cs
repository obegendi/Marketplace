using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.ProductServices.Queries.GetProductsWithTags
{
    public class GetProductsWithTagsQueryHandler : IQueryHandler<GetProductsWithTagsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsWithTagsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsWithTagsQuery request, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository.GetProductWithTagsAsync(request.Tags);

            var productList = productEntity.Select(x => new ProductDto
            {
                Description = x.Description,
                Images = x.Images,
                Tags = x.Tags,
                Sku = x.Sku,
                Name = x.Name
            });

            return productList;
        }
    }


}
