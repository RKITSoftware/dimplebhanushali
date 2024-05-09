using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Versoning_with_Query_String.BL;
using Versoning_with_Query_String.Models;

namespace Versoning_with_Query_String.Controllers
{
    /// <summary>
    /// Controller for API version 2 handling operations related to Customer2 entity.
    /// </summary>
    public class CLCustomerV2Controller : ApiController
    {
        // Create an instance of the Business Logic class
        private readonly BLCustomerV2 _customerV2BL = new BLCustomerV2();

        /// <summary>
        /// Retrieves the list of all customers for version 2.
        /// </summary>
        /// <returns>List of Customer2 entities.</returns>
        [HttpGet]
        public List<Customer2> GetAllCustomers()
        {
            // Use the Business Logic class method to get all customers
            return _customerV2BL.GetAllCustomers().ToList();
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The Customer2 entity with the specified identifier.</returns>
        [HttpGet]
        public IHttpActionResult GetCustomerById(int id)
        {
            // Use the Business Logic class method to get a customer by ID
            Customer2 customer = _customerV2BL.GetCustomerById(id);

            if (customer != null)
            {
                // Return OK with the customer data if found
                return Ok(customer);
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
            // Use the Business Logic class method to add a new customer
            Customer2 addedCustomer = _customerV2BL.AddCustomer(cust);

            if (addedCustomer != null)
            {
                // Return OK with the added customer data
                return Ok(addedCustomer);
            }

            // Return BadRequest if the input data is invalid
            return BadRequest("Invalid Data");
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
            // Use the Business Logic class method to edit an existing customer
            Customer2 editedCustomer = _customerV2BL.EditCustomer(id, cust);

            if (editedCustomer != null)
            {
                // Return OK with the updated customer data
                return Ok(editedCustomer);
            }

            // Return BadRequest if the input data is invalid
            return BadRequest("Invalid Data");
        }

        /// <summary>
        /// Deletes a customer based on their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A message indicating the successful deletion.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            // Use the Business Logic class method to delete a customer
            string deletionMessage = _customerV2BL.DeleteCustomer(id);

            if (deletionMessage != null)
            {
                // Return OK with a deletion message
                return Ok(deletionMessage);
            }

            // Return BadRequest if the input data is invalid
            return BadRequest("Invalid Data");
        }
    }
}
