namespace Marketplace.Application.CustomerServices.Commands.UpdateCustomerAddress
{
    public class UpdateCustomerAddressReq
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string FullAddress { get; set; }
    }
}
