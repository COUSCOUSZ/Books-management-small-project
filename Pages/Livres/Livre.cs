using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestionLivre.Pages.Livres
{
    public class Livre
    {
        public int IDLivre { get; set; }
        public string Titre { get; set; }
        
        public string ISBN { get; set; }
        public int IDEditeur { get; set; }
        public int IDAuteur { get; set; }
        public int IDCat { get; set; }
        public string NomEditeur { get; set; }
        public string NomAuteur { get; set; }
        public string NomCat { get; set; }
        public string DescripLivre { get; set; }
        public int? AnneeEdition { get; set; }

        string conString = "Data Source=4070V;Initial Catalog=GestionLivre;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";

        public Livre()
        {
        }

        public Livre(int idLivre, string titre, string isbn, int idEditeur, int idAuteur, int idCat, string descripLivre, int? anneeEdition)
        {
            IDLivre = idLivre;
            Titre = titre;
            ISBN = isbn;
            IDEditeur = idEditeur;
            IDAuteur = idAuteur;
            IDCat = idCat;
            DescripLivre = descripLivre;
            AnneeEdition = anneeEdition;
        }
        
        public Livre(int idLivre, string titre, string isbn, string nomEditeur, string nomAuteur, string nomCat, string descripLivre, int? anneeEdition)
        {
            IDLivre = idLivre;
            Titre = titre;
            ISBN = isbn;
            NomEditeur = nomEditeur;
            NomAuteur = nomAuteur;
            NomCat = nomCat;
            DescripLivre = descripLivre;
            AnneeEdition = anneeEdition;
        }

        public Livre(int idLivre,string titre){
            IDLivre = idLivre;
            Titre = titre;
        }

        public Livre(Livre livre)
        {
            IDLivre = livre.IDLivre;
            Titre = livre.Titre;
            ISBN = livre.ISBN;
            IDEditeur = livre.IDEditeur;
            IDAuteur = livre.IDAuteur;
            IDCat = livre.IDCat;
            DescripLivre = livre.DescripLivre;
            AnneeEdition = livre.AnneeEdition;
        }


        public int GetCount()
        {
            SqlConnection con = new(conString);
            int count = 0;
            con.Open();
            string sql = "SELECT count(*) from livre";
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
                Console.WriteLine("Exception livre " + ex.ToString());
            }
            return count;
        }
        public List<Livre> GetAllLivres()
        {
            List<Livre> listeLivres = new List<Livre>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT " +
                              "Livre.IDLivre, " +
                              "Livre.Titre, " +
                              "Livre.ISBN, " +
                              "Editeur.NomEditeur AS EditeurName, " +
                              "Auteur.NomAuteur AS AuteurName, " +
                              "Categorie.NomCat AS CategorieName, " +
                              "Editeur.IDEditeur AS EditeurID, " +
                              "Auteur.IDAuteur AS AuteurID, " +
                              "Categorie.IDCat AS CategorieID, " +
                              "Livre.DescripLivre, " +
                              "Livre.AnneeEdition " +
                              "FROM Livre " +
                              "INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur " +
                              "INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur " +
                              "INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat ";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Livre livre = new()
                            {
                                IDLivre = reader.GetInt32(0),
                                Titre = reader.GetString(1),
                                ISBN = reader.GetString(2),
                                NomEditeur = reader.GetString(3),
                                NomAuteur = reader.GetString(4),
                                NomCat = reader.GetString(5),
                                IDEditeur = reader.GetInt32(6),
                                IDAuteur = reader.GetInt32(7),
                                IDCat = reader.GetInt32(8),
                                DescripLivre = reader.GetString(9)
                            };
                            if (!reader.IsDBNull(10))
                                livre.AnneeEdition = reader.GetInt32(10);

                            listeLivres.Add(livre);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception livre " + ex.ToString());
                    }
                }
            }
            return listeLivres;
        }
        public List<Livre> GetTopLivres()
        {
            List<Livre> listeLivres = new List<Livre>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT TOP 3 " +
                              "Livre.IDLivre, " +
                              "Livre.Titre, " +
                              "Livre.ISBN, " +
                              "Editeur.NomEditeur AS EditeurName, " +
                              "Auteur.NomAuteur AS AuteurName, " +
                              "Categorie.NomCat AS CategorieName, " +
                              "Editeur.IDEditeur AS EditeurID, " +
                              "Auteur.IDAuteur AS AuteurID, " +
                              "Categorie.IDCat AS CategorieID, " +
                              "Livre.DescripLivre, " +
                              "Livre.AnneeEdition " +
                              "FROM Livre " +
                              "INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur " +
                              "INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur " +
                              "INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat ";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Livre livre = new Livre();
                            livre.IDLivre = reader.GetInt32(0);
                            livre.Titre = reader.GetString(1);
                            livre.ISBN = reader.GetString(2);
                            livre.NomEditeur = reader.GetString(3);
                            livre.NomAuteur = reader.GetString(4);
                            livre.NomCat = reader.GetString(5);
                            livre.IDEditeur = reader.GetInt32(6);
                            livre.IDAuteur = reader.GetInt32(7);
                            livre.IDCat = reader.GetInt32(8);
                            livre.DescripLivre = reader.GetString(9);
                            if (!reader.IsDBNull(10))
                                livre.AnneeEdition = reader.GetInt32(10);

                            listeLivres.Add(livre);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception livre " + ex.ToString());
                    }
                }
            }
            return listeLivres;
        }
        public List<Livre> SearchLivres(string searchTerm)
        {
            List<Livre> matchingLivres = new List<Livre>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT Livre.IDLivre, Livre.ISBN, NomEditeur AS EditeurName, Auteur.NomAuteur AS AuteurName, Categorie.NomCat AS CategorieName, Editeur.IDEditeur AS EditeurID, Auteur.IDAuteur AS AuteurID, Categorie.IDCat AS CategorieID, Livre.DescripLivre, Livre.AnneeEdition,Livre.Titre " +
                             "FROM Livre " +
                             "INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur " +
                             "INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur " +
                             "INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat " +
                             "WHERE CONCAT(Livre.ISBN, NomEditeur, Auteur.NomAuteur, Categorie.NomCat, Livre.DescripLivre, Livre.AnneeEdition,Livre.Titre) LIKE @searchTerm";
                Console.WriteLine(sql);
                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Livre livre = new Livre
                        {
                            IDLivre = reader.GetInt32(0),
                            ISBN = reader.GetString(1),
                            NomEditeur = reader.GetString(2),
                            NomAuteur = reader.GetString(3),
                            NomCat = reader.GetString(4),
                            IDEditeur = reader.GetInt32(5),
                            IDAuteur = reader.GetInt32(6),
                            IDCat = reader.GetInt32(7),
                            DescripLivre = reader.GetString(8),
                            AnneeEdition = reader.GetInt32(9),
                            Titre = reader.GetString(10)
                        };

                        matchingLivres.Add(livre);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception livre " + ex.ToString());
                }
            }

            return matchingLivres;
        }

        public Livre GetLivre(int idLivre)
        {
            Livre livre = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT " +
                             "Livre.IDLivre, " +
                             "Livre.Titre, " +
                             "Livre.ISBN, " +
                             "Editeur.NomEditeur AS EditeurName, " +
                             "Auteur.NomAuteur AS AuteurName, " +
                             "Categorie.NomCat AS CategorieName, " +
                             "Editeur.IDEditeur AS EditeurID, " +
                             "Auteur.IDAuteur AS AuteurID, " +
                             "Categorie.IDCat AS CategorieID, " +
                             "Livre.DescripLivre, " +
                             "Livre.AnneeEdition " +
                             "FROM Livre " +
                             "INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur " +
                             "INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur " +
                             "INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat " +
                             "WHERE Livre.IDLivre = @idLivre";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idLivre", idLivre);
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            livre = new Livre
                            {
                                IDLivre = reader.GetInt32(0),
                                Titre = reader.GetString(1),
                                ISBN = reader.GetString(2),
                                NomEditeur = reader.GetString(3),
                                NomAuteur = reader.GetString(4),
                                NomCat = reader.GetString(5),
                                IDEditeur = reader.GetInt32(6),
                                IDAuteur = reader.GetInt32(7),
                                IDCat = reader.GetInt32(8),
                                DescripLivre = reader.GetString(9)
                            };
                            if (!reader.IsDBNull(10))
                                livre.AnneeEdition = reader.GetInt32(10);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception livre " + ex.ToString());
                    }
                }
            }
            return livre;
        }


        public void AddLivre(Livre livre)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "INSERT INTO Livre (Titre, ISBN, IDEditeur, IDAuteur, IDCat, DescripLivre, AnneeEdition) " +
                         "VALUES (@Titre, @ISBN, @IDEditeur, @IDAuteur, @IDCat, @DescripLivre, @AnneeEdition)";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Titre", livre.Titre);
            cmd.Parameters.AddWithValue("@ISBN", livre.ISBN);
            cmd.Parameters.AddWithValue("@IDEditeur", livre.IDEditeur);
            cmd.Parameters.AddWithValue("@IDAuteur", livre.IDAuteur);
            cmd.Parameters.AddWithValue("@IDCat", livre.IDCat);
            cmd.Parameters.AddWithValue("@DescripLivre", livre.DescripLivre);
            cmd.Parameters.AddWithValue("@AnneeEdition", (object)livre.AnneeEdition ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void UpdateLivre(int idLivre, Livre livre)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "UPDATE Livre SET Titre = @Titre, ISBN = @ISBN, IDEditeur = @IDEditeur, " +
                             "IDAuteur = @IDAuteur, IDCat = @IDCat, DescripLivre = @DescripLivre, " +
                             "AnneeEdition = @AnneeEdition WHERE IDLivre = @IDLivre";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@IDLivre", idLivre);
                    cmd.Parameters.AddWithValue("@Titre", livre.Titre);
                    cmd.Parameters.AddWithValue("@ISBN", livre.ISBN);
                    cmd.Parameters.AddWithValue("@IDEditeur", livre.IDEditeur);
                    cmd.Parameters.AddWithValue("@IDAuteur", livre.IDAuteur);
                    cmd.Parameters.AddWithValue("@IDCat", livre.IDCat);
                    cmd.Parameters.AddWithValue("@DescripLivre", livre.DescripLivre);
                    cmd.Parameters.AddWithValue("@AnneeEdition", (object)livre.AnneeEdition ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteLivre(int idLivre)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "DELETE FROM Livre WHERE IDLivre = @IDLivre";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@IDLivre", idLivre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<string, int> GetLivresCountByCategory()
        {
            Dictionary<string, int> livresByCategory = new();
            using (SqlConnection con = new(conString))
            {
                con.Open();
                string sql = "SELECT NomCat, COUNT(*) as LivreCount FROM Livre INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat GROUP BY NomCat";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string categoryName = reader.GetString(0);
                            int livreCount = reader.GetInt32(1);
                            livresByCategory.Add(categoryName, livreCount);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception getting livres count by category: " + ex.ToString());
                    }
                }
            }
            return livresByCategory;
        }

    }
}
