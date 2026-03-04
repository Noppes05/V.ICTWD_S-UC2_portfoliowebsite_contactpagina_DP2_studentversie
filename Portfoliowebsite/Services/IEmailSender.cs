using Portfoliowebsite.Models;

namespace Portfoliowebsite.Services
{
    public interface IEmailSender
    {
        Task SendAsync(ContactFormulierViewModel model);
    }
}
