using GestionLivre.Pages.Auteurs;
using GestionLivre.Pages.Categories;
using GestionLivre.Pages.Editeurs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GestionLivre.Pages.Livres
{
    public class CreateModel : PageModel
    {
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

        public void OnGet()
        {
            // Populate the dropdown lists for Editeur, Auteur, and Categorie
            Editeurs = GetEditeurSelectList();
            Auteurs = GetAuteurSelectList();
            Categories = GetCategorieSelectList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            var newLivre = new Livre
            {
                Titre = Titre,
                ISBN = ISBN,
                IDEditeur = IDEditeur,
                IDAuteur = IDAuteur,
                IDCat = IDCat,
                DescripLivre = DescripLivre,
                AnneeEdition = AnneeEdition
            };

        Example: newLivre.AddLivre(newLivre);
            return RedirectToPage("Index");
        }


        // Helper method to get SelectListItems for Editeur dropdown
        private List<SelectListItem> GetEditeurSelectList()
        {
            var editeurs = new Editeur().GetAllEditeurs();
            var selectList = editeurs.Select(e => new SelectListItem { Value = e.IDEditeur.ToString(), Text = e.NomEditeur }).ToList();
            return selectList;
        }

        // Helper method to get SelectListItems for Auteur dropdown
        private List<SelectListItem> GetAuteurSelectList()
        {
            var auteurs = new Auteur().GetAllAuteurs();
            var selectList = auteurs.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.nom }).ToList();
            return selectList;
        }

        // Helper method to get SelectListItems for Categorie dropdown
        private List<SelectListItem> GetCategorieSelectList()
        {
            var categories = new Categorie().GetAllCategories();
            var selectList = categories.Select(c => new SelectListItem { Value = c.IDCat.ToString(), Text = c.NomCat }).ToList();
            return selectList;
        }
    }
}
