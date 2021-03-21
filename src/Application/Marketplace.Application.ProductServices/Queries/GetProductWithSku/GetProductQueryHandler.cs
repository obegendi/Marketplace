using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.ProductServices.Queries.GetProductWithSku
{
    public class GetProductQueryHandler : IQueryHandler<GetProductWithSkuQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductWithSkuQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetWithSkuAsync(request.Sku);
            if (product is null)
                throw new ProductNotFoundException();
            var productDto = new ProductDto
            {
                Sku = product.Sku,
                Description = product.Description,
                Images = product.Images,
                Name = product.Name,
                Tags = product.Tags
            };

            return productDto;
        }
    }
}
