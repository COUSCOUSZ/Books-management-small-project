using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Livres
{
    public class DetailsModel : PageModel
    {
        public Livre livre { get; set; }

        public void OnGet(int id)
        {
            livre = new Livre().GetLivre(id);
        }
    }
}
