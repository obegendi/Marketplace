using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.UpdateMerchantUser
{
    public class UpdateMerchantUserCommand : CommandBase<bool>
    {
        public UpdateMerchantUserCommand(Guid merchantCode, string userPhone, string phone, string email, string firstName, string lastName, string password,
            bool isActive, List<string> claims)
        {
            MerchantCode = merchantCode;
            UserPhone = userPhone;
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            IsActive = isActive;
            Claims = claims;
        }

        public Guid MerchantCode { get; }
        public string UserPhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<string> Claims { get; set; }
    }

}
