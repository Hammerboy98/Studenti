using Microsoft.AspNetCore.Identity;

namespace Studenti.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Puoi aggiungere altre proprietà personalizzate per l'utente, se necessario
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
    }
}
