using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Queries.GetMerchantProduct
{
    public class GetMerchantProductQueryHandler : IQueryHandler<GetMerchantProductQuery, MerchantProductDto>
    {
        private readonly IMerchantProductRepository _merchantProductRepository;

        public GetMerchantProductQueryHandler(IMerchantProductRepository merchantProductRepository)
        {
            _merchantProductRepository = merchantProductRepository;
        }

        public async Task<MerchantProductDto> Handle(GetMerchantProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _merchantProductRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode, request.Sku);
            if (product is null)
                throw new ProductNotFoundException();
            var productDto = new MerchantProductDto
            {
                Sku = product.Sku,
                Stock = product.Stock,
                IsInfiniteStock = product.IsInfiniteStock,
                Price = product.Price,
                Vat = product.Vat,
                PriceWithVat = product.PriceWithVat,
                MerchantAddressCode = request.MerchantAddressCode
            };

            return productDto;
        }
    }
}
