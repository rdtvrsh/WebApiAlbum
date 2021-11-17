using MCTunes.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MCTunes.Database
{
    public class RetrieveBrano 
    {
        string constr = @"Data Source=UMANA-5-MULTI\SQLEXPRESS01;Initial Catalog=DotNetPeTips; Database= MCTunes; Trusted_Connection=true;";
        public Brano Get(int id)
        {
            var brano = new Brano();
            string query = @"select Id, Anno as AnnoBrano, Titolo as TitoloBrano, Durata, IdAlbum
                                FROM Brano
                                WHERE Id=" + id;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (!sdr.Read()) return null;
                        brano = new Brano
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Anno = Convert.ToInt32(sdr["AnnoBrano"]),
                            Durata = Convert.ToDecimal(sdr["Durata"]),
                            Titolo = Convert.ToString(sdr["TitoloBrano"]),
                            Album_Id = Convert.ToInt32(sdr["IdAlbum"])

                        };
                    }
                    con.Close();
                }
            }
            
            return brano;
        }

        public IEnumerable<Brano> GetAllByBand(int idband)
        {
            var brano = new Brano();
            var lista = new List<Brano>();
            string query = @"select Id, Anno as AnnoBrano, Titolo as TitoloBrano, Durata, IdAlbum
                                FROM Brano b
                                JOIN Album a ON b.IdAlbum=a.Id
                                Join Band ba ON a.IdBand=ba.Id
                                WHERE ba.Id=" + idband;
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

                            do
                            {
                                brano = new Brano
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Anno = Convert.ToInt32(sdr["AnnoBrano"]),
                                    Durata = Convert.ToDecimal(sdr["Durata"]),
                                    Titolo = Convert.ToString(sdr["TitoloBrano"]),
                                    Album_Id = Convert.ToInt32(sdr["IdAlbum"])

                                };
                            } while (brano == null);
                            lista.Add(brano);
                        }
                    }
                    con.Close();
                }
            }
            return lista;
        }

        public bool NewBrano(Brano brano)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                //inserting Patient data into database
                string query = "insert into Brano values (@Titolo, @Anno,@Durata,@IdAlbum)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Titolo", brano.Titolo);
                    cmd.Parameters.AddWithValue("@Anno", Convert.ToInt32(brano.Anno));
                    cmd.Parameters.AddWithValue("@Durata", Convert.ToDecimal(brano.Durata));
                    cmd.Parameters.AddWithValue("@IdAlbum", Convert.ToInt32(brano.Album_Id));
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
    }
}
