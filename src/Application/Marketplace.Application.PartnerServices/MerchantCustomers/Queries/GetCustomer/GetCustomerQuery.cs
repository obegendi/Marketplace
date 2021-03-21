using System;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomer
{
    public class GetCustomerQuery : IQuery<CustomerDto>
    {
        public Guid Code { get; set; }

        public GetCustomerQuery(Guid code)
        {
            Code = code;
        }
    }
}
