using Marketplace.Common.Application.Queries;
using System;

namespace Marketplace.Application.CustomerServices.Queries.GetCustomer
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
