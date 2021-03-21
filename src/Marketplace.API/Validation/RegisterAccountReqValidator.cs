using FluentValidation;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.RegisterMerchantUser;

namespace Marketplace.API.Validation
{
    public class RegisterAccountReqValidator : AbstractValidator<RegisterMerchantReq>
    {
        public RegisterAccountReqValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.MerchantName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}
