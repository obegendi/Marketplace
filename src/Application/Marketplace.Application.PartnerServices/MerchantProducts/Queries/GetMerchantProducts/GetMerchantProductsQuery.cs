using System;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Queries.GetMerchantProducts
{
    public class GetMerchantProductsQuery : QueryBase<BaseListResponseModel<MerchantProductDto>>, IQuery<BaseListResponseModel<MerchantProductDto>>
    {
        public GetMerchantProductsQuery(Guid merchantCode, Guid merchantAddressCode, int skip, int limit, string search, string orderBy) : base(search, skip, limit, orderBy)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
    }
}
