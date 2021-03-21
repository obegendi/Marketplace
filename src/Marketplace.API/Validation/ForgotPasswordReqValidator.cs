using FluentValidation;
using Marketplace.Application.AuthenticationServices;

namespace Marketplace.API.Validation
{
    public class ForgotPasswordReqValidator : AbstractValidator<ForgotPasswordReq>
    {
        public ForgotPasswordReqValidator()
        {
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}
