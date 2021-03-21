namespace Marketplace.Application.CustomerServices.Commands.AddAddress
{
    public class AddAddressReq
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string FullAddress { get; set; }
    }
}
