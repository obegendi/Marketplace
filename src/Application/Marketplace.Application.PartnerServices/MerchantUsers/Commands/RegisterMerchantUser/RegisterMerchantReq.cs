namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.RegisterMerchantUser
{
    public class RegisterMerchantReq
    {
        public string MerchantName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
