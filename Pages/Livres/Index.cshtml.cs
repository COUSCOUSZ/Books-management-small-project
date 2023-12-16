using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace GestionLivre.Pages.Livres
{
    public class IndexModel : PageModel
    {
        public List<Livre> listeLivres = new List<Livre>();
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }

        public void OnGet()
        {
            Livre livre = new();
            if (!string.IsNullOrEmpty(search))
            {
                listeLivres = livre.SearchLivres(search);
            }
            else
            {
                listeLivres = livre.GetAllLivres();
            }
        }
    }
}
