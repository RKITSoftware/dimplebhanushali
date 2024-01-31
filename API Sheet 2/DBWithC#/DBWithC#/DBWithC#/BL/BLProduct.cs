using DBWithC_.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DBWithC_.BL
{
    /// <summary>
    /// Business logic for handling product-related operations.
    /// </summary>
    public class BLProduct
    {
        private static string _myConnection;

        /// <summary>
        /// Static constructor to initialize the database connection.
        /// </summary>
        static BLProduct()
        {
            _myConnection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>List of products.</returns>
        public List<prdct01> GetAll()
        {
            List<prdct01> products = new List<prdct01>();

            using (MySqlConnection connection = new MySqlConnection(_myConnection))
            {
                connection.Open();
                string query = @"SELECT 
                                        t01f01,
                                        t01f02,
                                        t01f03 
                                FROM 
                                        prdct01";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prdct01 product = new prdct01
                            {
                                t01f01 = Convert.ToInt32(reader["t01f01"]),
                                t01f02 = reader["t01f02"].ToString(),
                                t01f03 = Convert.ToDecimal(reader["t01f03"])
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        /// <summary>
        /// Retrieves a specific product by its ID from the database.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        public prdct01 GetProduct(int id)
        {
            prdct01 product = null;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                                        t01f01, 
                                        t01f02, 
                                        t01f03 
                                FROM 
                                        prdct01 
                                WHERE 
                                        t01f01 = @t01f01";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
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
                            };
                        }
                    }
                }
            }
            return product;
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="objProduct">The product to be added.</param>
        /// <returns>The added product with updated information (e.g., ID).</returns>
        public prdct01 AddProduct(prdct01 objProduct)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();
                string query = @"INSERT INTO 
                                            prdct01 
                                            (t01f02, 
                                            t01f03) 
                                VALUES 
                                            (@t01f02, 
                                            @t01f03)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t01f02", objProduct.t01f02);
                    cmd.Parameters.AddWithValue("@t01f03", objProduct.t01f03);

                    cmd.ExecuteNonQuery();

                    objProduct.t01f01 = (int)cmd.LastInsertedId;
                }
            }

            return objProduct;
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="id">ID of the product to be updated.</param>
        /// <param name="objProduct">Updated product information.</param>
        /// <returns>The updated product.</returns>
        public prdct01 EditProduct(int id, prdct01 objProduct)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();
                string query = @"UPDATE 
                                        prdct01 
                                SET 
                                        t01f02 = @t01f02, 
                                        t01f03 = @t01f03 
                                WHERE 
                                        t01f01 = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t01f02", objProduct.t01f02);
                    cmd.Parameters.AddWithValue("@t01f03", objProduct.t01f03);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            return objProduct;
        }

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="id">ID of the product to be deleted.</param>
        /// <returns>A message indicating the success of the deletion operation.</returns>
        public string DeleteProduct(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();
                string query = @"DELETE FROM 
                                            prdct01 
                                WHERE 
                                           t01f01 = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    return $"Product with Id => {id} Deleted";
                }
            }
        }
    }
}
