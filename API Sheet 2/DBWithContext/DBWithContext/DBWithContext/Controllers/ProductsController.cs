using DBWithContext.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace DBWithContext.Controllers
{
    public class ProductsController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        // GET api/products
        public IHttpActionResult GetProducts()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM prdct01";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<prdct01> products = new List<prdct01>();

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

                        return Ok(products);
                    }
                }
            }
        }

        // GET api/products/1
        public IHttpActionResult GetProduct(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM prdct01 WHERE t01f01 = @productId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            prdct01 product = new prdct01
                            {
                                t01f01 = Convert.ToInt32(reader["t01f01"]),
                                t01f02 = reader["t01f02"].ToString(),
                                t01f03 = Convert.ToDecimal(reader["t01f03"])
                            };

                            return Ok(product);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
        }

        // POST api/products
        public IHttpActionResult PostProduct(prdct01 newProduct)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO prdct01 (t01f02, t01f03) VALUES (@name, @price)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", newProduct.t01f02);
                    command.Parameters.AddWithValue("@price", newProduct.t01f03);

                    command.ExecuteNonQuery();
                }
            }

            return Ok(newProduct);
        }

        // PUT api/products/1
        public IHttpActionResult PutProduct(int id, prdct01 updatedProduct)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE prdct01 SET t01f02 = @name, t01f03 = @price WHERE t01f01 = @productId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", updatedProduct.t01f02);
                    command.Parameters.AddWithValue("@price", updatedProduct.t01f03);
                    command.Parameters.AddWithValue("@productId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(updatedProduct);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // DELETE api/products/1
        public IHttpActionResult DeleteProduct(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM prdct01 WHERE t01f01 = @productId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Product with ID {id} deleted");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
    }
}
