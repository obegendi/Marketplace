using FluentValidation;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.CreateMerchantUser;

namespace Marketplace.API.Validation
{
    public class AccountUserReqValidator : AbstractValidator<MerchantUserReq>
    {
        public AccountUserReqValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Claims).NotNull().NotEmpty();
        }
    }
}
