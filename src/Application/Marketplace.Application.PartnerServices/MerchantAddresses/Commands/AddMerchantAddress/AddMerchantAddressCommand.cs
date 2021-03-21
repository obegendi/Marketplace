using System;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.AddMerchantAddress
{
    public class AddMerchantAddressCommand : ICommand<MerchantAddressDto>
    {
        public AddMerchantAddressCommand(Guid merchantCode,
            string merchantName,
            string name,
            string address,
            string country,
            string city,
            string town,
            string district,
            string location,
            string workingHours,
            string extraInfo,
            bool isActive)
        {
            MerchantCode = merchantCode;
            MerchantName = merchantName;
            Name = name;
            Address = address;
            Country = country;
            City = city;
            Town = town;
            District = district;
            Location = location;
            WorkingHours = workingHours;
            ExtraInfo = extraInfo;
            IsActive = isActive;
        }

        public Guid MerchantCode { get; }
        public string MerchantName { get; }
        public string Name { get; }
        public string Address { get; }
        public string Country { get; }
        public string City { get; }
        public string Town { get; }
        public string District { get; }
        public string Location { get; }
        public string WorkingHours { get; }
        public string ExtraInfo { get; }
        public bool IsActive { get; }
        public Guid Id { get; }
    }
}
