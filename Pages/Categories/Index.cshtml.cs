using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public List<Categorie> categoriesList = new List<Categorie>();

        [BindProperty(SupportsGet = true)]
        public string search { get; set; }
        public string resultMessage="";

        public void OnGet()
        {
            Categorie categorie = new Categorie();
            // categoriesList = categorie.GetAllCategories();
            if (!string.IsNullOrEmpty(search))
            {
                categoriesList = categorie.SearchCategories(search);
            }
            else
            {
                categoriesList = categorie.GetAllCategories();
            }
            if (TempData.ContainsKey("CannotDelete"))
            {
                // Get the result message
                resultMessage = TempData["CannotDelete"].ToString();

                // Do something with the result message
            }
        }
    }
}
