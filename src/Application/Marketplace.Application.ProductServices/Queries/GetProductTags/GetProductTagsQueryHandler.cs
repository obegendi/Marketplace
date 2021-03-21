using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.ProductServices.Queries.GetProductTags
{
    public class GetProductTagsQueryHandler : IQueryHandler<GetProductTagsQuery, IEnumerable<ProductTagDto>>
    {
        private readonly IProductTagsRepository _productTagsRepository;

        public GetProductTagsQueryHandler(IProductTagsRepository productTagsRepository)
        {
            _productTagsRepository = productTagsRepository;
        }
        public async Task<IEnumerable<ProductTagDto>> Handle(GetProductTagsQuery request, CancellationToken cancellationToken)
        {
            var productTags = (await _productTagsRepository.GetAllAsync()).Select(x => new ProductTagDto { TagName = x.Name });

            return productTags;
        }
    }
}
