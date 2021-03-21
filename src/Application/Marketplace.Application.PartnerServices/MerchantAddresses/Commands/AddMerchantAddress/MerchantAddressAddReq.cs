namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.AddMerchantAddress
{
    public class MerchantAddressAddReq
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Location { get; set; }
        public string ExtraInfo { get; set; }
        public string WorkingHours { get; set; }
        public bool IsActive { get; set; }
    }
}
