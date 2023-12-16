using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Auteurs
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string Nom { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Telephone { get; set; }
        [BindProperty]
        public string Adresse { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            
            var newAuteur = new Auteur
            {
                nom = Nom,
                email = Email,
                telephone = Telephone,
                adresse = Adresse
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }


            newAuteur.AddAuteur(newAuteur);

            return RedirectToPage("Index");
        }
    }
}
