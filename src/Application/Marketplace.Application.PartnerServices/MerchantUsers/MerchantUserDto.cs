using System.Collections.Generic;

namespace Marketplace.Application.MerchantServices.MerchantUsers
{
    public class MerchantUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }

        public List<string> Claims { get; set; }
    }
}
