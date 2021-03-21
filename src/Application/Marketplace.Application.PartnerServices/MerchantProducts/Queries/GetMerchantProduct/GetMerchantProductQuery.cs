using System;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Queries.GetMerchantProduct
{
    public class GetMerchantProductQuery : IQuery<MerchantProductDto>
    {
        public GetMerchantProductQuery(Guid merchantCode, Guid merchantAddressCode, string sku)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            Sku = sku;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
        public string Sku { get; }
    }
}
