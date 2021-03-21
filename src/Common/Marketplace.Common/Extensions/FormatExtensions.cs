using System.Net.Mail;

namespace Marketplace.Common.Extensions
{
    public static class FormatExtensions
    {
        public static bool IsEmail(this string input)
        {
            try
            {
                var addr = new MailAddress(input);
                return addr.Address == input;
            }
            catch
            {
                return false;
            }
        }
    }
}
