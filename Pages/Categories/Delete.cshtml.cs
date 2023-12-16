using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        

        public IActionResult OnGet(int id)
        {
            var deletedCat=new Categorie();
            if(deletedCat.HasAssociatedLivres(id)){
                TempData["CannotDelete"] = "La catégorie a des livres associés et ne peut pas être supprimée. Veuillez d'abord supprimer la catégorie.";
                return RedirectToPage("Index");
            }
            deletedCat.DeleteCategory(id);
            return RedirectToPage("Index");
        }

        
    }
}
