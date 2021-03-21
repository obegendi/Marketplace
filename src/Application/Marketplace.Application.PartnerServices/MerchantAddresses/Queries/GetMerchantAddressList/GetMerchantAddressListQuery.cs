using Marketplace.API.Infrastructure;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common.Application.Queries;
using System;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddressList
{
    public class GetMerchantAddressListQuery : QueryBase<BaseListResponseModel<MerchantAddressDto>>, IQuery<BaseListResponseModel<MerchantAddressDto>>
    {
        public GetMerchantAddressListQuery(Guid merchantCode, string search, int skip, int limit, string orderBy) : base(search, skip, limit, orderBy)
        {
            MerchantCode = merchantCode;
        }

        public Guid MerchantCode { get; }
    }
}
