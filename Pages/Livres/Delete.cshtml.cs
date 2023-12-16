using GestionLivre.Pages.Editeurs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Livres
{
    public class DeleteModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            var deletedLivre = new Livre();
            deletedLivre.DeleteLivre(id);
            return RedirectToPage("Index");
        }
    }
}
