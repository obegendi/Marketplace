using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.CreateMerchantUser
{
    public class CreateMerchantUserCommand : CommandBase<MerchantUserDto>
    {
        public CreateMerchantUserCommand(Guid merchantCode, string merchantName, string firstName, string lastName, string password, string phone,
            string email,
            bool isActive, List<string> claims = null)
        {
            MerchantCode = merchantCode;
            MerchantName = merchantName;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            IsActive = isActive;
            Password = password;
            if (claims == null)
                Claims = new List<string>();
            else
                Claims = claims;
        }

        public Guid MerchantCode { get; }
        public string MerchantName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; }
        public string Password { get; set; }
        public List<string> Claims { get; set; }
    }

}
