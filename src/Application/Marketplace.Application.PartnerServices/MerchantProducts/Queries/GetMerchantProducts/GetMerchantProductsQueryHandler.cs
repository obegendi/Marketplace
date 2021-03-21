using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Queries.GetMerchantProducts
{
    public class GetMerchantProductsQueryHandler : IQueryHandler<GetMerchantProductsQuery, BaseListResponseModel<MerchantProductDto>>
    {
        private readonly IMerchantProductRepository _merchantProductRepository;

        public GetMerchantProductsQueryHandler(IMerchantProductRepository merchantProductRepository)
        {
            _merchantProductRepository = merchantProductRepository;
        }

        public async Task<BaseListResponseModel<MerchantProductDto>> Handle(GetMerchantProductsQuery request, CancellationToken cancellationToken)
        {
            var list = (await _merchantProductRepository
                    .GetBySearchAsync(request.MerchantCode, request.MerchantAddressCode, request.Limit, request.Skip, request.Search, request.OrderBy))
                .Select(x => new MerchantProductDto
                {
                    Name = x.Name,
                    IsActive = x.IsActive,
                    IsInfiniteStock = x.IsInfiniteStock,
                    MerchantAddressCode = x.MerchantAddressCode,
                    Price = x.Price,
                    PriceWithVat = x.PriceWithVat,
                    Sku = x.Sku,
                    Stock = x.Stock,
                    Vat = x.Vat
                }).ToList();

            return new BaseListResponseModel<MerchantProductDto>(request.Limit, list);
        }
    }
}
