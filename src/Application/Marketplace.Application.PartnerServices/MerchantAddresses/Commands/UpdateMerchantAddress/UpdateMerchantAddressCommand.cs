using System;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.UpdateMerchantAddress
{
    public class UpdateMerchantAddressCommand : CommandBase<MerchantAddressDto>
    {
        public UpdateMerchantAddressCommand(Guid merchantCode,
            Guid merchantAddressCode,
            string merchantName,
            string addressName,
            string address,
            string city,
            string town,
            string district,
            string location,
            string workingHours,
            string extraInfo,
            bool isActive)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            MerchantName = merchantName;
            Name = addressName;
            Address = address;
            City = city;
            Town = town;
            District = district;
            Location = location;
            WorkingHours = workingHours;
            ExtraInfo = extraInfo;
            IsActive = isActive;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
        public string MerchantName { get; }
        public string Name { get; }
        public string Address { get; }
        public string City { get; }
        public string Town { get; }
        public string District { get; }
        public string Location { get; }
        public string WorkingHours { get; set; }
        public string ExtraInfo { get; }
        public bool IsActive { get; }
    }
}
