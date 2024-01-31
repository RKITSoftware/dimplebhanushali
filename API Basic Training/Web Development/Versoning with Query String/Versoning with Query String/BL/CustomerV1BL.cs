using System.Collections.Generic;
using System.Linq;
using Versoning_with_Query_String.Models;

namespace Versoning_with_Query_String.BL
{
    /// <summary>
    /// Business Logic class for handling operations related to Customer1 entity.
    /// </summary>
    public class CustomerV1BL
    {
        /// <summary>
        /// Retrieves the list of all customers.
        /// </summary>
        /// <returns>List of Customer1 entities.</returns>
        public List<Customer1> GetAllCustomers()
        {
            return Customer1.lstCustomers1;
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The Customer1 entity with the specified identifier.</returns>
        public Customer1 GetCustomerById(int id)
        {
            return Customer1.lstCustomers1.FirstOrDefault(cust => cust.Id == id);
        }

        /// <summary>
        /// Adds a new customer to the list.
        /// </summary>
        /// <param name="cust">The Customer1 entity to be added.</param>
        /// <returns>The added Customer1 entity.</returns>
        public Customer1 AddCustomer(Customer1 cust)
        {
            if (cust != null)
            {
                // Assign a new ID to the customer and add to the list
                cust.Id = Customer1.lstCustomers1.Count + 1;
                Customer1.lstCustomers1.Add(cust);
            }

            return cust;
        }

        /// <summary>
        /// Edits an existing customer based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be edited.</param>
        /// <param name="cust">The updated Customer1 entity.</param>
        /// <returns>The updated Customer1 entity.</returns>
        public Customer1 EditCustomer(int id, Customer1 cust)
        {
            if (id != null)
            {
                // Find the existing customer with the given ID in the list
                Customer1 existingCust = Customer1.lstCustomers1.FirstOrDefault(c => c.Id == id);

                // Update the existing customer data
                if (existingCust != null)
                {
                    existingCust.PhoneNumber = cust.PhoneNumber;
                    existingCust.LastName = cust.LastName;
                    existingCust.FistName = cust.FistName;
                }

                return existingCust;
            }

            return null;
        }

        /// <summary>
        /// Deletes a customer based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A message indicating the successful deletion.</returns>
        public string DeleteCustomer(int id)
        {
            if (id != null)
            {
                // Remove the customer with the given ID from the list
                Customer1.lstCustomers1.Remove(Customer1.lstCustomers1.FirstOrDefault(c => c.Id == id));

                // Return a deletion message
                return $"Customer With id {id} Deleted !!!";
            }

            return "Invalid Data";
        }
    }
}
