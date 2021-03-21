using Marketplace.Common.Application.Commands;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : CommandBase<CustomerDto>
    {
        public Guid MerchantCode { get; set; }
        public Guid CustomerCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UpdatedBy { get; set; }
        public List<Address> Addresses { get; set; }

        public UpdateCustomerCommand(Guid merchantCode, Guid customerCode, string email, string phone, string firstName, string lastName, string updatedBy, List<Address> addresses)
        {
            MerchantCode = merchantCode;
            CustomerCode = customerCode;
            Email = email;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            UpdatedBy = updatedBy;
            Addresses = addresses;
        }
    }
}
