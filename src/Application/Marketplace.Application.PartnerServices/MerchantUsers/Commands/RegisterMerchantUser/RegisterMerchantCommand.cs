using Marketplace.Common.Application.Commands;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.RegisterMerchantUser
{
    public class RegisterMerchantCommand : CommandBase<Unit>
    {
        public RegisterMerchantCommand(string merchantName, string firstName, string lastName, string phone, string email, string password)
        {
            MerchantName = merchantName;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Password = password;
        }

        public string MerchantName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Password { get; }
    }

}
