using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.CustomerServices.Commands.UpdateCustomerAddress
{
    public class UpdateAddressCommand : CommandBase<bool>
    {
        public UpdateAddressCommand(Guid code, string email, string name, string city, string town, string district, string fullAddress, string updateBy)
        {
            Code = code;
            Email = email;
            Name = name;
            City = city;
            Town = town;
            District = district;
            FullAddress = fullAddress;
            UpdateBy = updateBy;
        }

        public Guid Code { get; set; }
        public string Email { get; }
        public string Name { get; }
        public string City { get; }
        public string Town { get; }
        public string District { get; }
        public string FullAddress { get; set; }
        public string UpdateBy { get; set; }
    }
}
