using System.Collections.Generic;
using System.Web.Http;
using Versoning_with_Query_String.BL;
using Versoning_with_Query_String.Models;

namespace Versoning_with_Query_String.Controllers
{
    /// <summary>
    /// Controller for API version 1 handling operations related to Customer1 entity.
    /// </summary>
    public class CLCustomerV1Controller : ApiController
    {
        private BLCustomerV1 _customerBL = new BLCustomerV1();

        /// <summary>
        /// Retrieves the list of all customers.
        /// </summary>
        /// <returns>List of Customer1 entities.</returns>
        [HttpGet]
        public List<Customer1> GetAllCustomers()
        {
            return _customerBL.GetAllCustomers();
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The Customer1 entity with the specified identifier.</returns>
        [HttpGet]
        public IHttpActionResult GetCustomerById(int id)
        {
            Customer1 customer = _customerBL.GetCustomerById(id);

            if (customer != null)
            {
                // Return OK with the customer data if found
                return Ok(customer);
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
            Customer1 addedCustomer = _customerBL.AddCustomer(cust);

            if (addedCustomer != null)
            {
                // Return OK with the added customer data
                return Ok(addedCustomer);
            }

            // Return BadRequest if the input data is invalid
            return BadRequest("Invalid Data");
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
            Customer1 editedCustomer = _customerBL.EditCustomer(id, cust);

            if (editedCustomer != null)
            {
                // Return OK with the updated customer data
                return Ok(editedCustomer);
            }

            // Return BadRequest if the input data is invalid
            return BadRequest("Invalid Data");
        }

        /// <summary>
        /// Deletes a customer based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A message indicating the successful deletion.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            string deletionMessage = _customerBL.DeleteCustomer(id);

            if (deletionMessage.Contains("Deleted"))
            {
                // Return OK with a deletion message
                return Ok(deletionMessage);
            }

            // Return BadRequest if the input data is invalid
            return BadRequest(deletionMessage);
        }
    }
}
