using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.RegisterCustomer
{
    public class RegisterCustomerCommand : CommandBase<CustomerDto>
    {
        public RegisterCustomerCommand(Guid merchantCode, string email, string phone, string firstName, string lastName, string createBy, List<Address> addresses)
        {
            MerchantCode = merchantCode;
            Email = email;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            CreateBy = createBy;
            Addresses = addresses;
        }

        public Guid MerchantCode { get; set; }
        public string Email { get; }
        public string Phone { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string CreateBy { get; set; }
        public List<Address> Addresses { get; }
    }
}
