using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : CommandBase
    {
        public ForgotPasswordCommand(string phone)
        {
            Phone = phone;
        }

        public string Phone { get; set; }
    }
}
