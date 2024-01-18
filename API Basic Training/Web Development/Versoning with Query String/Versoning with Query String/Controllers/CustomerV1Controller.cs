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
    /// Controller for API version 1 handling operations related to Customer1 entity.
    /// </summary>
    public class CustomerV1Controller : ApiController
    {
        /// <summary>
        /// Retrieves the list of all customers.
        /// </summary>
        /// <returns>List of Customer1 entities.</returns>
        [HttpGet]
        public List<Customer1> GetAllCustomers()
        {
            return Customer1.lstCustomers1;
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The Customer1 entity with the specified identifier.</returns>
        [HttpGet]
        public IHttpActionResult GetCustomerById(int id)
        {
            // Find the customer with the given ID in the list
            Customer1 objCustomer1 = Customer1.lstCustomers1.FirstOrDefault(cust => cust.Id == id);

            if (objCustomer1 != null)
            {
                // Return OK with the customer data if found
                return Ok(objCustomer1);
            }

            // Return NotFound if customer not found
            return NotFound();
        }

        /// <summary>
        /// Adds a new customer to the list.
        /// </summary>
        /// <param name="cust">The Customer1 entity to be added.</param>
        /// <returns>The added Customer1 entity.</returns>
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer1 cust)
        {
            if (cust == null)
            {
                // Return BadRequest if the input data is invalid
                return BadRequest("Invalid Data");
            }

            // Assign a new ID to the customer and add to the list
            cust.Id = Customer1.lstCustomers1.Count + 1;
            Customer1.lstCustomers1.Add(cust);

            // Return OK with the added customer data
            return Ok(cust);
        }

        /// <summary>
        /// Edits an existing customer based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be edited.</param>
        /// <param name="cust">The updated Customer1 entity.</param>
        /// <returns>The updated Customer1 entity.</returns>
        [HttpPut]
        public IHttpActionResult EditCustomer(int id, Customer1 cust)
        {
            if (id == null)
            {
                // Return BadRequest if the input data is invalid
                return BadRequest("Invalid Data");
            }

            // Find the existing customer with the given ID in the list
            Customer1 existingCust = Customer1.lstCustomers1.FirstOrDefault(c => c.Id == id);

            // Update the existing customer data
            existingCust.PhoneNumber = cust.PhoneNumber;
            existingCust.LastName = cust.LastName;
            existingCust.FistName = cust.FistName;

            // Return OK with the updated customer data
            return Ok(existingCust);
        }

        /// <summary>
        /// Deletes a customer based on their unique identifier.
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
            Customer1.lstCustomers1.Remove(Customer1.lstCustomers1.FirstOrDefault(c => c.Id == id));

            // Return OK with a deletion message
            return Ok($"Customer With id {id} Deleted !!!");
        }
    }
}
