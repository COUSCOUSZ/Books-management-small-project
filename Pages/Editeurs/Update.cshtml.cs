using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Editeurs
{
    public class UpdateModel : PageModel
    {
        public Editeur editeurTemp = new Editeur();
        public Editeur existingEditeur = null;

        [BindProperty]
        public int Id { get; set; }
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

        public void OnGet(int id)
        {
            existingEditeur = new Editeur(editeurTemp.GetEditeur(id));
            Id = existingEditeur.IDEditeur;
            Nom = existingEditeur.NomEditeur;
            Description = existingEditeur.DescripEditeur;
            Email = existingEditeur.EmailEditeur;
            Telephone = existingEditeur.TelephoneEditeur;
            Adresse = existingEditeur.AdresseEditeur;
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedEditeur = new Editeur
            (
                Id,
                Nom,
                Description,
                Email,
                Telephone,
                Adresse
            );

            updatedEditeur.UpdateEditeur(id, updatedEditeur);

            return RedirectToPage("Index");
        }
    }
}
