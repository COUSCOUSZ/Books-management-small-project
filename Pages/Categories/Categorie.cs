using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestionLivre.Pages.Livres;

namespace GestionLivre.Pages.Categories
{
    public class Categorie
    {
        public int IDCat;
        public string NomCat;
        public string? DescriptionCat;

        // Connection string for the database
        private readonly string conString = "Data Source=4070V;Initial Catalog=GestionLivre;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";

        // Constructors
        public Categorie()
        {
        }

        public Categorie(int idCat, string nomCat, string? descriptionCat)
        {
            this.IDCat = idCat;
            this.NomCat = nomCat;
            this.DescriptionCat = descriptionCat;
        }

        public Categorie(Categorie cat)
        {
            IDCat = cat.IDCat;
            NomCat = cat.NomCat;
            DescriptionCat = cat.DescriptionCat;
        }

        // CRUD operations
        public int GetCount()
        {
            SqlConnection con = new(conString);
            int count = 0;
            con.Open();
            string sql = "SELECT count(*) from categorie";
            SqlCommand cmd = new(sql, con);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception categorie " + ex.ToString());
            }
            return count;
        }
        public List<Categorie> GetAllCategories()
        {
            List<Categorie> categoriesList = new List<Categorie>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Categorie";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Categorie categorie = new Categorie();
                            categorie.IDCat = reader.GetInt32(0);
                            categorie.NomCat = reader.GetString(1);
                            categorie.DescriptionCat = reader.IsDBNull(2) ? null : reader.GetString(2);
                            categoriesList.Add(categorie);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception categorie " + ex.ToString());
                    }
                }
            }
            return categoriesList;
        }
        public List<Categorie> SearchCategories(string search)
        {
            List<Categorie> categories = new List<Categorie>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Categorie WHERE CONCAT(NomCat, DescriptionCat) LIKE @search ORDER BY NomCat DESC";
                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@search", $"%{search}%");
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Categorie categorie = new Categorie
                        {
                            IDCat = reader.GetInt32(0),
                            NomCat = reader.GetString(1),
                            DescriptionCat = reader.GetString(2)
                        };
                        categories.Add(categorie);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception searching categories: " + ex.ToString());
                }
            }
            return categories;
        }



        public Categorie GetCategory(int searchID)
        {
            Categorie category = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Categorie WHERE IDCat = @id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", searchID);
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            category = new Categorie();
                            category.IDCat = reader.GetInt32(0);
                            category.NomCat = reader.GetString(1);
                            category.DescriptionCat = reader.IsDBNull(2) ? null : reader.GetString(2);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception categorie " + ex.ToString());
                    }
                }
            }
            return category;
        }

        public void AddCategory(Categorie category)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "INSERT INTO Categorie (NomCat, DescriptionCat) VALUES (@NomCat, @DescriptionCat)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@NomCat", category.NomCat);
                    cmd.Parameters.AddWithValue("@DescriptionCat", category.DescriptionCat);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCategory(int urlId, Categorie category)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "UPDATE Categorie SET NomCat = @NomCat, DescriptionCat = @DescriptionCat WHERE IDCat = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", urlId);
                    cmd.Parameters.AddWithValue("@NomCat", category.NomCat);
                    cmd.Parameters.AddWithValue("@DescriptionCat", category.DescriptionCat);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCategory(int urlId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "DELETE FROM Categorie WHERE IDCat = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", urlId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<string> GetAllCategoriesNames()
        {
            List<string> categoryNames = new List<string>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "select distinct NomCat from categorie C join Livre L on C.IDCat=L.IDCat";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            categoryNames.Add(reader.GetString(0));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception getting category names: " + ex.ToString());
                    }
                }
            }
            return categoryNames;
        }
        public bool HasAssociatedLivres(int IDCat)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "SELECT COUNT(*) FROM Livre WHERE IDCat = @IDCat";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@IDCat", IDCat);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public List<Livre> GetAssociatedLivres(int CatId)
        {
            List<Livre> listeLivres = new();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Livre WHERE IDCat = @idCat";
                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@idCat", CatId);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Livre livre = new Livre
                        {
                            IDLivre = reader.GetInt32(0),
                            Titre = reader.GetString(1)
                        };
                        listeLivres.Add(livre);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception categorie " + ex.ToString());
                }
            }
            return listeLivres;
        }
    }
}
