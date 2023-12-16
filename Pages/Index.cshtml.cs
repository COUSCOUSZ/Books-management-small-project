using GestionLivre.Pages.Auteurs;
using GestionLivre.Pages.Categories;
using GestionLivre.Pages.Editeurs;
using GestionLivre.Pages.Livres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public int AuteurCount { get; set; }
        public int EditeurCount { get; set; }
        public int CategorieCount { get; set; }
        public int LivreCount { get; set; }
        public int recordsCount {get;set;}

        readonly Auteur auteur = new();
        readonly Categorie categorie = new();
        readonly Editeur editeur = new();
        readonly Livre livre = new();
        public List<Livre> listeLivres = new List<Livre>();
        public List<Auteur> listeAuteurs = new List<Auteur>();
        public List<Livre> topListeLivres = new List<Livre>();
        public List<Auteur> topListeAuteurs = new List<Auteur>();
        public List<Categorie> listeCategories = new List<Categorie>();
         public List<string> CategoriesForChart { get; set; }
        public Dictionary<string, int> LivresByCategory { get; set; }

        public void OnGet()
        {
            AuteurCount = auteur.GetCount();
            EditeurCount = editeur.GetCount();
            CategorieCount = categorie.GetCount();
            LivreCount = livre.GetCount();
            recordsCount=AuteurCount+EditeurCount+CategorieCount+LivreCount;
            
            listeLivres = livre.GetAllLivres();
            listeAuteurs = auteur.GetAllAuteurs();
            listeCategories=categorie.GetAllCategories();

            topListeLivres = livre.GetTopLivres();
            topListeAuteurs = auteur.GetTopAuteurs();

            
            CategoriesForChart = categorie.GetAllCategoriesNames();  // Assuming you have a method to get category names
            LivresByCategory = livre.GetLivresCountByCategory();  // Assuming you have a method to get livre counts by category
        }
    }
}