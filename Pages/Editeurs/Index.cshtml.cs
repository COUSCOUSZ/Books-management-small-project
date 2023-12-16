using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace GestionLivre.Pages.Editeurs
{
    public class IndexModel : PageModel
    {
        public List<Editeur> listeEditeurs = new();

        [BindProperty(SupportsGet = true)]
        public string search { get; set; }

        public string resultMessage="";
        public void OnGet()
        {
            Editeur editeur = new Editeur();
            // listeEditeurs = editeur.GetAllEditeurs();
            if (!string.IsNullOrEmpty(search))
            {
                listeEditeurs = editeur.SearchEditeurs(search);
            }
            else
            {
                listeEditeurs = editeur.GetAllEditeurs();
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
