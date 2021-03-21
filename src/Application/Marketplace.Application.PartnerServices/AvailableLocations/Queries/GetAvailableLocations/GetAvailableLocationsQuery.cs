using System;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Queries.GetAvailableLocations
{
    public class GetAvailableLocationsQuery : QueryBase<BaseListResponseModel<Location>>
    {
        public GetAvailableLocationsQuery(Guid merchantCode, Guid merchantAddressCode, string search, int skip, int limit, string orderBy) : base(search, skip, limit, orderBy)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
        }
        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
    }
}
