using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using workbench.Models;

public class PersonalDetailsController : ApiController
{
    private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

    // GET api/PersonalDetails
    public IEnumerable<Person> Get()
    {
        List<Person> persons = new List<Person>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Persons";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person person = new Person
                        {
                            Id = reader.GetInt32("Id"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Email = reader.GetString("Email"),
                            Gender = reader.GetString("Gender")
                        };

                        persons.Add(person);
                    }
                }
            }
        }

        return persons;
    }

    // GET api/PersonalDetails/5
    public IHttpActionResult Get(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Persons WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Person person = new Person
                        {
                            Id = reader.GetInt32("Id"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Email = reader.GetString("Email"),
                            Gender = reader.GetString("Gender")
                        };

                        return Ok(person);
                    }
                }
            }
        }

        return NotFound();
    }

    // POST api/PersonalDetails
    public IHttpActionResult Post([FromBody] Person person)
    {
        if (person == null)
            return BadRequest("Invalid data");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Persons (FirstName, LastName, Email, Gender) VALUES (@FirstName, @LastName, @Email, @Gender)";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Gender", person.Gender);

                cmd.ExecuteNonQuery();
            }
        }

        return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
    }

    // PUT api/PersonalDetails/5
    public IHttpActionResult Put(int id, [FromBody] Person updatedPerson)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "UPDATE Persons SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", updatedPerson.FirstName);
                cmd.Parameters.AddWithValue("@LastName", updatedPerson.LastName);
                cmd.Parameters.AddWithValue("@Email", updatedPerson.Email);
                cmd.Parameters.AddWithValue("@Gender", updatedPerson.Gender);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    return Ok(updatedPerson);
            }
        }

        return NotFound();
    }

    // DELETE api/PersonalDetails/5
    public IHttpActionResult Delete(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "DELETE FROM Persons WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    return Ok($"Person with Id {id} deleted successfully");
            }
        }

        return NotFound();
    }
}
