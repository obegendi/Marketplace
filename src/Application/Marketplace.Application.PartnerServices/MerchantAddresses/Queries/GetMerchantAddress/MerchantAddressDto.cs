using System;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress
{
    public class MerchantAddressDto
    {
        public Guid Code { get; set; }
        public string MerchantName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Location { get; set; }
        public string ExtraInfo { get; set; }
        public string WorkingHours { get; set; }
        public bool IsActive { get; set; }
    }
}
