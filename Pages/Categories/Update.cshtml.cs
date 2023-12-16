using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Categories
{
    public class UpdateModel : PageModel
    {
        public Categorie categoryTemp = new Categorie();
        public Categorie existingCategory = null;

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string NomCat { get; set; }
        [BindProperty]
        public string DescriptionCat { get; set; }

        public void OnGet(int id)
        {
            existingCategory = new Categorie(categoryTemp.GetCategory(id));
            Id = existingCategory.IDCat;
            NomCat = existingCategory.NomCat;
            DescriptionCat = existingCategory.DescriptionCat;
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedCategory = new Categorie
            {
                IDCat = Id,
                NomCat = NomCat,
                DescriptionCat = DescriptionCat
            };

            updatedCategory.UpdateCategory(id, updatedCategory);

            return RedirectToPage("Index");
        }
    }
}
