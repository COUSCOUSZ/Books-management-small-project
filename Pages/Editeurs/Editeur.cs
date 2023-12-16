using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestionLivre.Pages.Livres;

namespace GestionLivre.Pages.Editeurs
{
    public class Editeur
    {
        public int IDEditeur { get; set; }
        public string NomEditeur { get; set; }
        public string DescripEditeur { get; set; }
        public string EmailEditeur { get; set; }
        public string TelephoneEditeur { get; set; }
        public string AdresseEditeur { get; set; }

        private readonly string conString = "Data Source=4070V;Initial Catalog=GestionLivre;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";

        public Editeur()
        {
        }

        public Editeur(int id, string nom, string description, string email, string telephone, string adresse)
        {
            IDEditeur = id;
            NomEditeur = nom;
            DescripEditeur = description;
            EmailEditeur = email;
            TelephoneEditeur = telephone;
            AdresseEditeur = adresse;
        }

        public Editeur(Editeur editeur)
        {
            IDEditeur = editeur.IDEditeur;
            NomEditeur = editeur.NomEditeur;
            DescripEditeur = editeur.DescripEditeur;
            EmailEditeur = editeur.EmailEditeur;
            TelephoneEditeur = editeur.TelephoneEditeur;
            AdresseEditeur = editeur.AdresseEditeur;
        }

        public int GetCount()
        {
            SqlConnection con = new(conString);
            int count = 0;
            con.Open();
            string sql = "SELECT count(*) from editeur";
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
                Console.WriteLine("Exception editeurs " + ex.ToString());
            }
            return count;
        }

        public List<Editeur> GetAllEditeurs()
        {
            List<Editeur> listeEditeurs = new List<Editeur>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Editeur";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Editeur editeur = new Editeur();
                            editeur.IDEditeur = reader.GetInt32(0);
                            editeur.NomEditeur = reader.GetString(1);
                            editeur.DescripEditeur = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            editeur.EmailEditeur = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            editeur.TelephoneEditeur = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            editeur.AdresseEditeur = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                            listeEditeurs.Add(editeur);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Editeur: " + ex.ToString());
                    }
                }
            }
            return listeEditeurs;
        }
        public List<Editeur> SearchEditeurs(string search)
        {
            List<Editeur> matchingEditeurs = new List<Editeur>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Editeur WHERE CONCAT(NomEditeur, DescripEditeur, TelephoneEditeur, AdresseEditeur,EmailEditeur) LIKE @search ORDER BY NomEditeur DESC";

                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@search", $"%{search}%");

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Editeur editeur = new Editeur
                        {
                            IDEditeur = reader.GetInt32(0),
                            NomEditeur = reader.GetString(1),
                            DescripEditeur = reader.GetString(2),
                            EmailEditeur = reader.GetString(3),
                            TelephoneEditeur = reader.GetString(4),
                            AdresseEditeur = reader.GetString(5)
                        };

                        matchingEditeurs.Add(editeur);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception editeur " + ex.ToString());
                }
            }

            return matchingEditeurs;
        }


        public Editeur GetEditeur(int searchID)
        {
            Editeur editeur = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Editeur WHERE IDEditeur = @id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", searchID);
                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            editeur = new Editeur();
                            editeur.IDEditeur = reader.GetInt32(0);
                            editeur.NomEditeur = reader.GetString(1);
                            editeur.DescripEditeur = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            editeur.EmailEditeur = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            editeur.TelephoneEditeur = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            editeur.AdresseEditeur = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Editeur: " + ex.ToString());
                    }
                }
            }
            return editeur;
        }

        public void AddEditeur(Editeur editeur)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "INSERT INTO Editeur (NomEditeur, DescripEditeur, EmailEditeur, telephoneEditeur, AdresseEditeur) " +
                         "VALUES (@Nom, @Description, @Email, @Telephone, @Adresse)";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Nom", editeur.NomEditeur);
                cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(editeur.DescripEditeur) ? (object)DBNull.Value : editeur.DescripEditeur);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(editeur.EmailEditeur) ? (object)DBNull.Value : editeur.EmailEditeur);
                cmd.Parameters.AddWithValue("@Telephone", string.IsNullOrEmpty(editeur.TelephoneEditeur) ? (object)DBNull.Value : editeur.TelephoneEditeur);
                cmd.Parameters.AddWithValue("@Adresse", string.IsNullOrEmpty(editeur.AdresseEditeur) ? (object)DBNull.Value : editeur.AdresseEditeur);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEditeur(int id, Editeur editeur)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "UPDATE Editeur SET NomEditeur = @Nom, DescripEditeur = @Description, EmailEditeur = @Email, " +
                             "telephoneEditeur = @Telephone, AdresseEditeur = @Adresse WHERE IDEditeur = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nom", editeur.NomEditeur);
                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(editeur.DescripEditeur) ? (object)DBNull.Value : editeur.DescripEditeur);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(editeur.EmailEditeur) ? (object)DBNull.Value : editeur.EmailEditeur);
                    cmd.Parameters.AddWithValue("@Telephone", string.IsNullOrEmpty(editeur.TelephoneEditeur) ? (object)DBNull.Value : editeur.TelephoneEditeur);
                    cmd.Parameters.AddWithValue("@Adresse", string.IsNullOrEmpty(editeur.AdresseEditeur) ? (object)DBNull.Value : editeur.AdresseEditeur);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEditeur(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "DELETE FROM Editeur WHERE IDEditeur = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool HasAssociatedLivres(int editeurId)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "SELECT COUNT(*) FROM Livre WHERE IDEditeur = @editeurId";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@editeurId", editeurId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public List<Livre> GetAssociatedLivres(int editeurId)
        {
            List<Livre> listeLivres = new();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Livre WHERE IDEditeur = @idediteur";
                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@idediteur", editeurId);
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
                    Console.WriteLine("Exception editeur " + ex.ToString());
                }
            }
            return listeLivres;
        }
    }
}
