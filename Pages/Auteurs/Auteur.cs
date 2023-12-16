using System.Data.SqlClient;
using GestionLivre.Pages.Livres;

namespace GestionLivre.Pages.Auteurs
{
    public class Auteur
    {
        public int id;
        public string? nom;
        public string? email;
        public string? telephone;
        public string? adresse;
        string conString = "Data Source=4070V;Initial Catalog=GestionLivre;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";

        public Auteur()
        {
        }

        public Auteur(int id, string? nom, string? email, string? telephone, string? adresse)
        {
            this.id = id;
            this.nom = nom;
            this.email = email;
            this.telephone = telephone;
            this.adresse = adresse;
        }

        public Auteur(Auteur a)
        {
            id = a.id;
            nom = a.nom;
            email = a.email;
            telephone = a.telephone;
            adresse = a.adresse;
        }

        public int GetCount()
        {
            SqlConnection con = new(conString);
            int count = 0;
            con.Open();
            string sql = "SELECT count(*) from Auteur";
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
                Console.WriteLine("Exception auteur " + ex.ToString());
            }
            return count;
        }

        public List<Auteur> GetAllAuteurs()
        {
            List<Auteur> listeAuteurs = new List<Auteur>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM auteur";
                using SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Auteur auteur = new Auteur
                        {
                            id = reader.GetInt32(0),
                            nom = reader.GetString(1),
                            email = reader.GetString(2),
                            telephone = reader.GetString(3),
                            adresse = reader.GetString(4)
                        };
                        listeAuteurs.Add(auteur);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception auteur " + ex.ToString());
                }
            }
            return listeAuteurs;
        }
        public List<Auteur> GetTopAuteurs()
        {
            List<Auteur> listeAuteurs = new List<Auteur>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT TOP 3 * FROM auteur";
                using SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Auteur auteur = new Auteur
                        {
                            id = reader.GetInt32(0),
                            nom = reader.GetString(1),
                            email = reader.GetString(2),
                            telephone = reader.GetString(3),
                            adresse = reader.GetString(4)
                        };
                        listeAuteurs.Add(auteur);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception auteur " + ex.ToString());
                }
            }
            return listeAuteurs;
        }

        public Auteur GetAuteur(int searchID)
        {
            Auteur auteur = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM auteur WHERE IDAuteur =@id";
                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", searchID);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        auteur = new Auteur
                        {
                            id = reader.GetInt32(0),
                            nom = reader.GetString(1),
                            email = reader.GetString(2),
                            telephone = reader.GetString(3),
                            adresse = reader.GetString(4)
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception auteur " + ex.ToString());
                }
            }
            return auteur;
        }

        public List<Auteur> SearchAuteurs(string search)
        {
            List<Auteur> result = new List<Auteur>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Auteur WHERE CONCAT(NomAuteur,EmailAuteur,TelephoneAuteur,AdresseAuteur)  LIKE @search ORDER BY NomAuteur DESC";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Auteur auteur = new Auteur
                            {
                                id = reader.GetInt32(0),
                                nom = reader.GetString(1),
                                email = reader.GetString(2),
                                telephone = reader.GetString(3),
                                adresse = reader.GetString(4)
                            };
                            result.Add(auteur);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception during search: " + ex.ToString());
                    }
                }
            }

            return result;
        }


        public void AddAuteur(Auteur auteur)
        {
            using SqlConnection con = new(conString);
            con.Open();
            string sql = "INSERT INTO Auteur (NomAuteur, EmailAuteur, TelephoneAuteur, AdresseAuteur) VALUES (@Nom, @Email, @Telephone, @Adresse)";
            using SqlCommand cmd = new(sql, con);
            Console.WriteLine("--->" + auteur.nom);
            cmd.Parameters.AddWithValue("@Nom", auteur.nom);
            cmd.Parameters.AddWithValue("@Email", auteur.email);
            cmd.Parameters.AddWithValue("@Telephone", auteur.telephone);
            cmd.Parameters.AddWithValue("@Adresse", auteur.adresse);

            cmd.ExecuteNonQuery();
        }

        public void UpdateAuteur(int urlId, Auteur auteur)
        {

            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "UPDATE Auteur SET NomAuteur = @Nom, EmailAuteur = @Email, TelephoneAuteur = @Telephone, AdresseAuteur = @Adresse WHERE IDAuteur = @Id";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", urlId);
            cmd.Parameters.AddWithValue("@Nom", auteur.nom);
            cmd.Parameters.AddWithValue("@Email", auteur.email);
            cmd.Parameters.AddWithValue("@Telephone", auteur.telephone);
            cmd.Parameters.AddWithValue("@Adresse", auteur.adresse);
            Console.WriteLine("--------->" + urlId);
            cmd.ExecuteNonQuery();
        }

        public void UpdateAuteur(int id, string nom, string email, string telephone, string adresse)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "UPDATE Auteur SET NomAuteur = @Nom, EmailAuteur = @Email, TelephoneAuteur = @Telephone, AdresseAuteur = @Adresse WHERE IDAuteur = @Id";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nom", nom);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Telephone", telephone);
            cmd.Parameters.AddWithValue("@Adresse", adresse);
            Console.WriteLine("--------->" + id);
            Console.WriteLine("--------->" + sql);
            cmd.ExecuteNonQuery();
        }

        public void DeleteAuteur(int urlId)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "DELETE auteur where IDAuteur=@Id";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", urlId);
            Console.WriteLine("--------->" + urlId);
            cmd.ExecuteNonQuery();
        }
        public bool HasAssociatedLivres(int auteurId)
        {
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "SELECT COUNT(*) FROM Livre WHERE IDAuteur = @AuteurId";
            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AuteurId", auteurId);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public List<Livre> GetAssociatedLivres(int auteurId)
        {
            List<Livre> listeLivres = new ();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string sql = "SELECT * FROM Livre WHERE IDAuteur = @idAuteur";
                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@idAuteur", auteurId);
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
                    Console.WriteLine("Exception auteur " + ex.ToString());
                }
            }
            return listeLivres;
        }
    }
}
