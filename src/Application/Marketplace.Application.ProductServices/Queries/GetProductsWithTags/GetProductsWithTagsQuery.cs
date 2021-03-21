using System.Collections.Generic;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.ProductServices.Queries.GetProductsWithTags
{
    public class GetProductsWithTagsQuery : IQuery<IEnumerable<ProductDto>>
    {
        public GetProductsWithTagsQuery(List<string> tags)
        {
            Tags = tags;
        }

        public List<string> Tags { get; }
    }

}
