using System.Collections.Generic;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.CreateMerchantUser
{
    public class MerchantUserReq
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<string> Claims { get; set; }
    }
}
