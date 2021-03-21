using System.Threading.Tasks;

namespace Marketplace.API.Infrastructure.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
