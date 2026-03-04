using Portfoliowebsite.Models;
using System.Net;
using System.Net.Mail;

namespace Portfoliowebsite.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        public async Task SendAsync(ContactFormulierViewModel model)
        {
            var smtp = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("2cecdf54e47e7c", "d112b12b0406e4"),
                EnableSsl = true
            };

            var mail = new MailMessage();
            mail.From = new MailAddress("noreply@example.com", "Website");

            mail.To.Add("contact@example.com");

            mail.Subject = $"Contact: {model.Onderwerp}";
            mail.Body = $"Naam: {model.Naam}\nEmail: {model.Email}\nBericht:\n{model.Bericht}";

            await smtp.SendMailAsync(mail);
        }
    }
}
