using MCTunes.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MCTunes.Database
{
    public class RetrieveAlbum 
    {
        string constr = @"Data Source=UMANA-5-MULTI\SQLEXPRESS01;Initial Catalog=DotNetPeTips; Database= MCTunes; Trusted_Connection=true;";
        public Album Get(int id)
        {
            var album = new Album();
            string query = @"select a.*,b.Id as IdBrano,b.Anno as AnnoBrano, b.Titolo as TitoloBrano, b.Durata
                                FROM Album a
                                JOIN Brano b ON a.Id = b.IdAlbum
                                WHERE a.Id=" + id;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (album.Titolo == null)
                            {
                                album = new Album
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Anno = Convert.ToInt32(sdr["Anno"]),
                                    Band_Id = Convert.ToInt32(sdr["IdBand"]),
                                    Genere = Convert.ToString(sdr["Genere"]),
                                    Titolo = Convert.ToString(sdr["Titolo"])
                                };
                            }
                            
                            var brano = AddBrano(Convert.ToInt32(sdr["IdBrano"]), sdr["TitoloBrano"].ToString(), Convert.ToInt32(sdr["AnnoBrano"]), Convert.ToInt32(sdr["Id"]), Convert.ToDecimal(sdr["Durata"]));
                            album.Brani.Add(brano);
                        }
                    }
                    con.Close();
                }
            }
            if (album.Titolo == null)
            {
                throw new Exception("Aggiungi prima i brani cojone!");
            }
            return album;
        }

        public IEnumerable<Album> GetAllByBand(int idband)
        {
            var album = new Album();
            var lista = new List<Album>();
            string query = @"select a.*,b.Id as IdBrano,b.Anno as AnnoBrano, b.Titolo as TitoloBrano, b.Durata
                                FROM Album a
                                JOIN Brano b ON a.Id = b.IdAlbum
                                WHERE a.IdBand=" + idband;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        
                        while (sdr.Read())
                        {
                            var idalbum = Convert.ToInt32(sdr["Id"]);
                            if (lista.Where(x => x.Id == idalbum).SingleOrDefault() == null)
                            {
                                album = new Album
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Anno = Convert.ToInt32(sdr["Anno"]),
                                    Band_Id = Convert.ToInt32(sdr["IdBand"]),
                                    Genere = Convert.ToString(sdr["Genere"]),
                                    Titolo = Convert.ToString(sdr["Titolo"])
                                };
                                lista.Add(album);
                            }
                           
                            var brano = AddBrano(Convert.ToInt32(sdr["IdBrano"]), sdr["TitoloBrano"].ToString(), Convert.ToInt32(sdr["AnnoBrano"]), Convert.ToInt32(sdr["Id"]), Convert.ToDecimal(sdr["Durata"]));
                            album.Brani.Add(brano);
                        }
                    }
                    con.Close();
                }
            }
            return lista;
        }

        public bool NewAlbum(Album album)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                //inserting Patient data into database
                string query = "insert into Album values (@Titolo, @Anno,@Genere,@IdBand)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Titolo", album.Titolo);
                    cmd.Parameters.AddWithValue("@Anno", Convert.ToInt32(album.Anno));
                    cmd.Parameters.AddWithValue("@Genere", album.Genere);
                    cmd.Parameters.AddWithValue("@IdBand", Convert.ToInt32(album.Band_Id));
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
                    }

                    con.Close();
                    return false;
                }
            }
        }
        private static Brano AddBrano(int id, string titolo, int anno, int albumId, decimal durata) => new Brano()
        {
            Album_Id = albumId,
            Anno = anno,
            Durata = durata,
            Titolo = titolo,
            Id = id
        };
    }

}

