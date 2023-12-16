using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionLivre.Pages.Auteurs
{
    public class UpdateModel : PageModel
    {
        public Auteur auteurTemp = new Auteur();
        public Auteur existingAuteur = null;

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Nom { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Telephone { get; set; }
        [BindProperty]
        public string Adresse { get; set; }


        public void OnGet(int id)
        {
            existingAuteur = new Auteur(auteurTemp.GetAuteur(id));
            Id = existingAuteur.id;
            Nom = existingAuteur.nom;
            Email = existingAuteur.email;
            Telephone = existingAuteur.telephone;
            Adresse = existingAuteur.adresse;
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedAuteur = new Auteur
            (
                Id,
                Nom,
                Email,
                Telephone,
                Adresse
            );

            updatedAuteur.UpdateAuteur(id,updatedAuteur);

            return RedirectToPage("Index");
        }
    }
}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace GestionLivre.Pages.Auteurs
//{
//    public class UpdateModel : PageModel
//    {
//        public Auteur auteurTemp = new Auteur();

//        public Auteur auteur = null;


//        public void OnGet(int id)
//        {
//            auteur = new Auteur(auteurTemp.GetAuteur(id));
//        }

//        public void OnPost( int id)
//        {
//            string nom = Request.Form["nom"];
//            string email = Request.Form["email"];
//            string telephone = Request.Form["telephone"];
//            string adresse = Request.Form["adresse"];

//            auteur.UpdateAuteur(id,nom,email,telephone,adresse);

//            Response.Redirect("Index");
//        }
//    }
//}

