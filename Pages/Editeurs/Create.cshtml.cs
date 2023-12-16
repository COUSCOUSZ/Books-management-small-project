using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Editeurs
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string Nom { get; set; }
        [BindProperty]
        public string Description { get; set; }
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
            var newEditeur = new Editeur
            {
                NomEditeur = Nom,
                DescripEditeur = Description,
                EmailEditeur = Email,
                TelephoneEditeur = Telephone,
                AdresseEditeur = Adresse
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newEditeur.AddEditeur(newEditeur);

            return RedirectToPage("Index");
        }
    }
}
