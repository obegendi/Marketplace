namespace Marketplace.Application.AuthenticationServices
{
    public class LoginReq
    {
        public string Password { get; set; }
        public string EmailOrPhone { get; set; }
        public int ExpiryInDay { get; set; }
    }

}
