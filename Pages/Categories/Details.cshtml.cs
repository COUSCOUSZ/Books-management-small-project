using GestionLivre.Pages.Livres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        public Categorie categoryTemp = new Categorie();
        public Categorie category = null;

         public List<Livre> listeLivres = new();

        public void OnGet(int id)
        {
            category = new Categorie(categoryTemp.GetCategory(id));
            listeLivres=category.GetAssociatedLivres(id);
        }
    }
}
