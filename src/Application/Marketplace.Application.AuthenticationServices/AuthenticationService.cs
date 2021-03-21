using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Common;
using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.Application.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _secretKey;
        private readonly IMerchantUserRepository _userRepository;

        public AuthenticationService(IMerchantUserRepository userRepository, IOptions<Appsettings> options)
        {
            _userRepository = userRepository;
            _secretKey = options.Value.Keys.TokenSecret;
        }
        public async Task<UserDto> Authenticate(string EmailOrPhone, string password, int expiryInDay)
        {
            var isEmail = EmailOrPhone.IsEmail();
            var user = new MerchantUser();
            if (isEmail)
                user = await _userRepository.GetWithEmailAsync(EmailOrPhone, password.AsMd5());
            else
                user = await _userRepository.GetWithPhoneAsync(EmailOrPhone, password.AsMd5());

            // return null if user not found
            if (user == null)
                throw new MerchantUserNotFoundException();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.MobilePhone, user.Phone),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(nameof(user.MerchantCode), user.MerchantCode.ToString()),
                    new Claim(nameof(user.MerchantName), user.MerchantName)
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userDto = new UserDto
            {
                Token = $"Bearer {tokenHandler.WriteToken(token)}",
                Claims = user.Claims,
                Email = user.Email,
                Phone = user.Phone,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userDto;
        }

        public async Task<string> Refresh(string token, IEnumerable<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            //var username = principal.Identity.Name;
            //var savedRefreshToken = GetRefreshToken(username); //retrieve the refresh token from a data store
            //if (savedRefreshToken != refreshToken)
            //    throw new SecurityTokenException("Invalid refresh token");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.MobilePhone, claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value),
                    new Claim(ClaimTypes.Email, claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value),
                    new Claim(ClaimTypes.Name, claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value),
                    new Claim(ClaimTypes.Surname, claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value),
                    new Claim("MerchantCode", claims.FirstOrDefault(x => x.Type == "MerchantCode")?.Value),
                    new Claim("MerchantName", claims.FirstOrDefault(x => x.Type == "MerchantName")?.Value)
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var generatedToken = tokenHandler.CreateToken(tokenDescriptor);

            return $"Bearer {tokenHandler.WriteToken(generatedToken)}";
        }
    }
}
