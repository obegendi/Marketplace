using System.Collections.Generic;
using Marketplace.Common.Application.Commands;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.CustomerServices.Commands.RegisterCustomer
{
    public class RegisterCustomerCommand : CommandBase<CustomerDto>
    {
        public RegisterCustomerCommand(string email, string phone, string name, string surname, string createBy,  List<Address> addresses)
        {
            Email = email;
            Phone = phone;
            Name = name;
            Surname = surname;
            CreateBy = createBy;
            Addresses = addresses;
        }

        public string Email { get; }
        public string Phone { get; }
        public string Name { get; }
        public string Surname { get; }
        public string CreateBy { get; set; }
        public List<Address> Addresses { get; }
    }
}
