using System.Collections.Generic;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.UpdateCustomer
{
    public class UpdateCustomerReq
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
    }
}