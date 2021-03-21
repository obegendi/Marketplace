using System.Collections.Generic;

namespace Marketplace.Application.AuthenticationServices
{
    public class UserDto
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<string> Claims { get; set; }
    }
}
