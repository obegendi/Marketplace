using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.ProductServices.Queries.GetProducts
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, BaseListResponseModel<ProductDto>>
    {
        private readonly IMapper _map;
        private readonly IProductRepository _productRepository;
        private int _limit;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper map)
        {
            _productRepository = productRepository;
            _map = map;
        }

        public async Task<BaseListResponseModel<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _limit = request.Limit;
            if (_limit > 10 || _limit < 1)
                _limit = 10;
            _limit += 1;

            var productList =
                await _productRepository.GetAllAsync($"{string.Join(" ", request.Tags)} {request.Search}", request.Skip, request.Limit, request.OrderBy);
            var response = _map.Map<List<ProductDto>>(productList);

            return new BaseListResponseModel<ProductDto>(_limit, response);
        }
    }
}
