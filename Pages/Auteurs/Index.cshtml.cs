using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace GestionLivre.Pages.Auteurs
{
    public class IndexModel : PageModel
    {

        public List<Auteur> listeAuteurs = new List<Auteur>();
        
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }

        public string resultMessage="";

        public void OnGet()
        {
            Auteur auteur = new Auteur();
            // listeAuteurs = auteur.GetAllAuteurs();

            if (!string.IsNullOrEmpty(search))
            {
                // Perform the search based on the provided term
                listeAuteurs = auteur.SearchAuteurs(search);
            }
            else
            {
                // If no search term is provided, get all auteurs
                listeAuteurs = auteur.GetAllAuteurs();
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
