using FluentValidation;
using Marketplace.Application.AuthenticationServices;

namespace Marketplace.API.Validation
{
    public class LoginReqValidator : AbstractValidator<LoginReq>
    {
        public LoginReqValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.EmailOrPhone).NotEmpty();
            RuleFor(x => x.ExpiryInDay).NotEmpty();
        }
    }
}
