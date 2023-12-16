using GestionLivre.Pages.Livres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Auteurs
{
    public class DetailsModel : PageModel
    {
        public Auteur auteurTemp = new Auteur();
        public Auteur auteur = null;
        public List<Livre> listeLivres = new();

        public void OnGet(int id)
        {
            auteur = new Auteur(auteurTemp.GetAuteur(id));
            listeLivres=auteur.GetAssociatedLivres(id);
        }
    }
}
