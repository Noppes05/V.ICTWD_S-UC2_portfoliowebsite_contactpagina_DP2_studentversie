using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Models;
using Portfoliowebsite.Services;
using System.Text.Encodings.Web;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {

        private readonly IEmailSender _email;
        public ContactController(IEmailSender email) => _email = email;

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactFormulierViewModel model)
        {
            // Valideer het model en toon fouten indien nodig
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Encodeer de invoer om XSS-aanvallen te voorkomen
            model.Naam = HtmlEncoder.Default.Encode(model.Naam);
            model.Email = HtmlEncoder.Default.Encode(model.Email);
            if (model.Onderwerp != null)
                model.Onderwerp = HtmlEncoder.Default.Encode(model.Onderwerp);
            if (model.Bericht != null)
                model.Bericht = HtmlEncoder.Default.Encode(model.Bericht);

            // Controleer op CRLF-injectie in het e-mailadres om header-injectie te voorkomen
            if (model.Email.Contains("\r") || model.Email.Contains("\n"))
            {
                ModelState.AddModelError("", "Ongeldig e-mailadres.");
                return View(model);
            }
            // Probeer het e-mailbericht te verzenden en handel eventuele fouten af
            try
            {
                await _email.SendAsync(model);
                TempData["ThanksName"] = model.Naam;
                TempData["ThanksEmail"] = model.Email;
                TempData["ThanksMessage"] = model.Bericht;

                return RedirectToAction(nameof(Thanks));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Er is een fout opgetreden bij het verzenden van uw bericht";
                return View(model);
            }
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
