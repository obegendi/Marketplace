using System;
using System.Collections.Generic;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.MerchantCustomers
{
    public class CustomerDto
    {
        public Guid Code { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
