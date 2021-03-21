namespace Marketplace.API.Infrastructure.Email
{
    public class EmailMessage
    {
        public EmailMessage(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
        }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }
}
