using GestionLivre.Pages.Livres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Editeurs
{
    public class DetailsModel : PageModel
    {
        public Editeur editeurTemp = new Editeur();
        public Editeur editeur = null;
        public List<Livre> listeLivres = new();

        public void OnGet(int id)
        {
            editeur = new Editeur(editeurTemp.GetEditeur(id));
            listeLivres=editeur.GetAssociatedLivres(id);
        }
    }
}
