using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Versoning_with_Query_String.Models;

namespace Versoning_with_Query_String.Controllers
{
    /// <summary>
    /// Controller for API version 2 handling operations related to Customer2 entity.
    /// </summary>
    public class CustomerV2Controller : ApiController
    {
        /// <summary>
        /// Retrieves the list of all customers for version 2.
        /// </summary>
        /// <returns>List of Customer2 entities.</returns>
        [HttpGet]
        public List<Customer2> GetAllCustomers()
        {
            return Customer2.lstCustomers2;
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The Customer2 entity with the specified identifier.</returns>
        [HttpGet]
        public IHttpActionResult GetCustomerById(int id)
        {
            // Find the customer with the given ID in the list
            Customer2 objCustomer2 = Customer2.lstCustomers2.FirstOrDefault(cust => cust.Id == id);

            if (objCustomer2 != null)
            {
                // Return OK with the customer data if found
                return Ok(objCustomer2);
            }

            // Return NotFound if customer not found
            return NotFound();
        }

        /// <summary>
        /// Adds a new customer to the list for version 2.
        /// </summary>
        /// <param name="cust">The Customer2 entity to be added.</param>
        /// <returns>The added Customer2 entity.</returns>
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer2 cust)
        {
            if (cust == null)
            {
                // Return BadRequest if the input data is invalid
                return BadRequest("Invalid Data");
            }

            // Assign a new ID to the customer and add to the list
            cust.Id = Customer2.lstCustomers2.Count + 1;
            Customer2.lstCustomers2.Add(cust);

            // Return OK with the added customer data
            return Ok(cust);
        }

        /// <summary>
        /// Edits an existing customer based on their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be edited.</param>
        /// <param name="cust">The updated Customer2 entity.</param>
        /// <returns>The updated Customer2 entity.</returns>
        [HttpPut]
        public IHttpActionResult EditCustomer(int id, Customer2 cust)
        {
            if (id == null)
            {
                // Return BadRequest if the input data is invalid
                return BadRequest("Invalid Data");
            }

            // Find the existing customer with the given ID in the list
            Customer2 existingCust = Customer2.lstCustomers2.FirstOrDefault(c => c.Id == id);

            // Update the existing customer data
            existingCust.PhoneNumber = cust.PhoneNumber;
            existingCust.Name = cust.Name;
            existingCust.Gender = cust.Gender;

            // Return OK with the updated customer data
            return Ok(existingCust);
        }

        /// <summary>
        /// Deletes a customer based on their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A message indicating the successful deletion.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (id == null)
            {
                // Return BadRequest if the input data is invalid
                return BadRequest("Invalid Data");
            }

            // Remove the customer with the given ID from the list
            Customer2.lstCustomers2.Remove(Customer2.lstCustomers2.FirstOrDefault(c => c.Id == id));

            // Return OK with a deletion message
            return Ok($"Customer With id {id} Deleted !!!");
        }
    }
}
