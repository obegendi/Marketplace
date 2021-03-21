using System;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress
{
    public class GetMerchantAddressQuery : IQuery<MerchantAddressDto>
    {
        public GetMerchantAddressQuery(Guid merchantCode, Guid merchantAddressCode)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
    }
}
