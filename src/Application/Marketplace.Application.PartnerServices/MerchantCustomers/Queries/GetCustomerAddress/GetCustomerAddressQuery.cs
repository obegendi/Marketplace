using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomerAddress
{
    public class GetCustomerAddressQuery : QueryBase<BaseListResponseModel<Address>>
    {
        public Guid Code { get; set; }

        public GetCustomerAddressQuery(Guid code, string search, int skip, int limit, string orderBy) : base(search, skip, limit, orderBy)
        {
            Code = code;
        }
    }
}
