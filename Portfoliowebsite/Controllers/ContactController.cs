using Microsoft.AspNetCore.Mvc;
using Portfoliowebsite.Models;
using Portfoliowebsite.Services;

namespace Portfoliowebsite.Controllers
{
    public class ContactController : Controller
    {

        private readonly IEmailSender _email;
        public ContactController(IEmailSender email) => _email = email;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormulierViewModel model)
        {
            if (!ModelState.IsValid) { 
            return View(model);
            }
            await _email.SendAsync(model);

            TempData["ThanksName"] = model.Naam;
            TempData["ThanksEmail"] = model.Email;
            TempData["ThanksMessage"] = model.Bericht;

            return RedirectToAction(nameof(Thanks));
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
