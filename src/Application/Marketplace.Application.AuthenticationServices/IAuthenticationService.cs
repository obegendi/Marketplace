using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Marketplace.Application.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<UserDto> Authenticate(string EmailOrPhone, string password, int expiryInDay);
        Task<string> Refresh(string token, IEnumerable<Claim> claims);
    }
}
