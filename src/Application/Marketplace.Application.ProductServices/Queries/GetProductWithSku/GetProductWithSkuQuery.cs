using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.ProductServices.Queries.GetProductWithSku
{
    public class GetProductWithSkuQuery : IQuery<ProductDto>
    {
        public GetProductWithSkuQuery(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }
}
