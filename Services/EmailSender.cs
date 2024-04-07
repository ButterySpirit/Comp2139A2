using Microsoft.AspNetCore.Identity.UI.Services;

using SendGrid;

using SendGrid.Helpers.Mail;

namespace Assign1.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _sendGridKey;

        public EmailSender(IConfiguration configuration)
        {
            _sendGridKey = configuration["SendGrid:ApiKey"];

        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_sendGridKey);
            var from = new EmailAddress("ian.mcdonald@georgebrown.ca", "JourneyNest");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            await client.SendEmailAsync(msg);
        }
    }
}
