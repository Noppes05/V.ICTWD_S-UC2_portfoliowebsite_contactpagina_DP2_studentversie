using System.ComponentModel.DataAnnotations;

namespace Portfoliowebsite.Models
{
    public class ContactFormulierViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(100, ErrorMessage = "Naam mag niet langer zijn dan 100 tekens")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig emailadres")]
        [StringLength(150, ErrorMessage = "Email mag niet langer zijn dan 150 tekens")]
        public string Email { get; set; }

        [StringLength(250, ErrorMessage = "Bericht mag maximaal 250 tekens bevatten.")]
        public string? Onderwerp { get; set; }

        [StringLength(1000, ErrorMessage = "Bericht mag maximaal 100 tekens bevatten.")]
        public string? Bericht { get; set; }
    }
}
