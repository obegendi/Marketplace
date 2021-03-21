using System.Collections.Generic;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.CustomerServices.Commands.RegisterCustomer
{
    public class RegisterCustomerReq
    {
        public string Email { get; }
        public string Phone { get; }
        public string Name { get; }
        public string Surname { get; }
        public List<Address> Addresses { get; set; }
    }
}
