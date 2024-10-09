using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Productapp_with_SQL.Models
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }

    }
}
