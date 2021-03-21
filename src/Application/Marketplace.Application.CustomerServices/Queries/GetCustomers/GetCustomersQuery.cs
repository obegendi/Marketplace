using System;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.CustomerServices.Queries.GetCustomers
{
    public class GetCustomersQuery : QueryBase<BaseListResponseModel<CustomerDto>>
    {
        public Guid MerchantCode { get; set; }

        public GetCustomersQuery(Guid merchantCode, string search, int skip, int limit, string orderBy) : base(search, skip, limit, orderBy)
        {
            MerchantCode = merchantCode;
        }
    }
}
