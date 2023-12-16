using GestionLivre.Pages.Auteurs;
using GestionLivre.Pages.Categories;
using GestionLivre.Pages.Editeurs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestionLivre.Pages.Livres
{
    public class UpdateModel : PageModel
    {
        private readonly Livre _livreService = new Livre();

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Titre { get; set; }

        [BindProperty]
        public string ISBN { get; set; }

        [BindProperty]
        public int IDEditeur { get; set; }

        [BindProperty]
        public int IDAuteur { get; set; }

        [BindProperty]
        public int IDCat { get; set; }

        [BindProperty]
        public string DescripLivre { get; set; }

        [BindProperty]
        public int? AnneeEdition { get; set; }

        public List<SelectListItem> Editeurs { get; set; }
        public List<SelectListItem> Auteurs { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public void OnGet(int id)
        {
            var livre = _livreService.GetLivre(id);

            if (livre == null)
            {
                return;
            }

            Id = livre.IDLivre;
            Titre = livre.Titre;
            ISBN = livre.ISBN;
            IDEditeur = livre.IDEditeur;
            IDAuteur = livre.IDAuteur;
            IDCat = livre.IDCat;
            DescripLivre = livre.DescripLivre;
            AnneeEdition = livre.AnneeEdition;

            // Populate the dropdown lists for Editeur, Auteur, and Categorie
            Editeurs = GetEditeurSelectList();
            Auteurs = GetAuteurSelectList();
            Categories = GetCategorieSelectList();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                OnGet(id);
                return Page();
            }

            var updatedLivre = new Livre
            {
                IDLivre = id,
                Titre = Titre,
                ISBN = ISBN,
                IDEditeur = IDEditeur,
                IDAuteur = IDAuteur,
                IDCat = IDCat,
                DescripLivre = DescripLivre,
                AnneeEdition = AnneeEdition
            };

            _livreService.UpdateLivre(id, updatedLivre);

            return RedirectToPage("Index");
        }

        // Helper methods for getting SelectListItems for dropdown lists
        private List<SelectListItem> GetEditeurSelectList()
        {
            var editeurs = new Editeur().GetAllEditeurs();
            return editeurs.Select(e => new SelectListItem { Value = e.IDEditeur.ToString(), Text = e.NomEditeur }).ToList();
        }

        private List<SelectListItem> GetAuteurSelectList()
        {
            var auteurs = new Auteur().GetAllAuteurs();
            return auteurs.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.nom }).ToList();
        }

        private List<SelectListItem> GetCategorieSelectList()
        {
            var categories = new Categorie().GetAllCategories();
            return categories.Select(c => new SelectListItem { Value = c.IDCat.ToString(), Text = c.NomCat }).ToList();
        }
    }
}
