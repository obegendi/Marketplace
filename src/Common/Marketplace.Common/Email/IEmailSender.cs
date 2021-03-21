using System.Threading.Tasks;

namespace Marketplace.Common.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
