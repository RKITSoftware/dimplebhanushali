using DBWithC_.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DBWithC_.BL
{
    public class BLProduct
    {
        private static string _myConnection;
        static BLProduct()
        {
            _myConnection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public List<prdct01> GetAll()
        {
            List<prdct01> products = new List<prdct01>();

            using (MySqlConnection connection = new MySqlConnection(_myConnection))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM prdct01", connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prdct01 product = new prdct01
                            {
                                // Populate properties based on your database schema
                                t01f01 = Convert.ToInt32(reader["t01f01"]),
                                t01f02 = reader["t01f02"].ToString(),
                                t01f03 = Convert.ToDecimal(reader["t01f03"])
                                // Add other properties as needed
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        public prdct01 GetProduct(int id)
        {
            prdct01 product = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM prdct01 WHERE t01f01 = @t01f01", conn))
                {
                    cmd.Parameters.AddWithValue("@t01f01", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new prdct01
                            {
                                t01f01 = Convert.ToInt32(reader["t01f01"]),
                                t01f02 = reader["t01f02"].ToString(),
                                t01f03 = Convert.ToDecimal(reader["t01f03"])
                                // Add other properties as needed
                            };
                        }
                    }
                }
            }
            return product;
        }

        public prdct01 AddProduct(prdct01 objProduct)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO prdct01 (t01f02, t01f03) VALUES (@t01f02, @t01f03)", conn))
                {
                    cmd.Parameters.AddWithValue("@t01f02", objProduct.t01f02);
                    cmd.Parameters.AddWithValue("@t01f03", objProduct.t01f03);

                    cmd.ExecuteNonQuery();

                    // Optionally, you can retrieve the ID of the inserted record if needed
                    objProduct.t01f01 = (int)cmd.LastInsertedId;
                }
            }

            return objProduct;
        }

        public prdct01 EditProduct(int id, prdct01 objProduct)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("UPDATE prdct01 SET t01f02 = @t01f02, t01f03 = @t01f03 WHERE t01f01 = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@t01f02", objProduct.t01f02);
                    cmd.Parameters.AddWithValue("@t01f03", objProduct.t01f03);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            return objProduct;
        }

        public string DeleteProduct(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM prdct01 WHERE t01f01 = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    return $"Product with Id => {id} Deleted";
                }
            }
        }
    }
}