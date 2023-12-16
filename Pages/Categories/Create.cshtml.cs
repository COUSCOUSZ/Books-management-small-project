using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string NomCat { get; set; }
        [BindProperty]
        public string DescriptionCat { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var newCategory = new Categorie
            {
                NomCat = NomCat,
                DescriptionCat = DescriptionCat
            };

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newCategory.AddCategory(newCategory);

            return RedirectToPage("Index");
        }
    }
}
