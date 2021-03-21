using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.CustomerServices.Commands.AddAddress
{
    public class AddAddressCommand : CommandBase<bool>
    {
        public AddAddressCommand(Guid code, string name, string city, string town, string district, string fullAddress)
        {
            Code = code;
            Name = name;
            City = city;
            Town = town;
            District = district;
            FullAddress = fullAddress;
        }

        public Guid Code { get; set; }
        public string Name { get; }
        public string City { get; }
        public string Town { get; }
        public string District { get; }
        public string FullAddress { get; }
    }
}
