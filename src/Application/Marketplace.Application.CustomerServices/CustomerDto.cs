using System;
using System.Collections.Generic;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.CustomerServices
{
    public class CustomerDto
    {
        public Guid Code { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
