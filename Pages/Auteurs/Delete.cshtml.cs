using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Auteurs
{
    public class DeleteModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            var deletedAuteur = new Auteur();

            if (deletedAuteur.HasAssociatedLivres(id))
            {
                TempData["CannotDelete"] = "L'auteur a associé des livres et ne peut pas être supprimé. Veuillez d'abord supprimer ses livres.";
                return RedirectToPage("Index");
            }
            else
            {
                deletedAuteur.DeleteAuteur(id);

                return RedirectToPage("Index");
            }
        }
    }
}
