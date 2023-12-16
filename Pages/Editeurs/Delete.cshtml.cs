using GestionLivre.Pages.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Editeurs
{
    public class DeleteModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            var deletedEditeur = new Editeur();
            if (deletedEditeur.HasAssociatedLivres(id)){
                TempData["CannotDelete"] = "L'éditeur a associé des livres et ne peut pas être supprimé. Veuillez d'abord supprimer ses livres.";
                return RedirectToPage("Index");
            }
            deletedEditeur.DeleteEditeur(id);
            return RedirectToPage("Index");
        }
    }
}
