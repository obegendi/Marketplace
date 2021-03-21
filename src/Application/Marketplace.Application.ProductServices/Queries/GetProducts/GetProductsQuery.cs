using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.ProductServices.Queries.GetProducts
{
    public class GetProductsQuery : IQuery<BaseListResponseModel<ProductDto>>
    {
        public GetProductsQuery(string search, string[] tags, int skip, int limit, string orderBy)
        {
            Search = search;
            Tags = tags;
            Skip = skip;
            Limit = limit;
            OrderBy = orderBy;
        }

        public string Search { get; }
        public string[] Tags { get; }
        public int Skip { get; }
        public int Limit { get; set; }
        public string OrderBy { get; }
    }
}
