using System.Linq;
using Versoning_with_Query_String.Models;

namespace Versoning_with_Query_String.BL
{
    /// <summary>
    /// Business Logic class for API version 2 handling operations related to Customer2 entity.
    /// </summary>
    public class BLCustomerV2
    {
        /// <summary>
        /// Retrieves the list of all customers for version 2.
        /// </summary>
        /// <returns>List of Customer2 entities.</returns>
        public IQueryable<Customer2> GetAllCustomers()
        {
            return Customer2.lstCustomers2.AsQueryable();
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The Customer2 entity with the specified identifier.</returns>
        public Customer2 GetCustomerById(int id)
        {
            // Find the customer with the given ID in the list
            return Customer2.lstCustomers2.FirstOrDefault(cust => cust.Id == id);
        }

        /// <summary>
        /// Adds a new customer to the list for version 2.
        /// </summary>
        /// <param name="cust">The Customer2 entity to be added.</param>
        /// <returns>The added Customer2 entity.</returns>
        public Customer2 AddCustomer(Customer2 cust)
        {
            if (cust != null)
            {
                // Assign a new ID to the customer and add to the list
                cust.Id = Customer2.lstCustomers2.Count + 1;
                Customer2.lstCustomers2.Add(cust);

                return cust;
            }

            return null;
        }

        /// <summary>
        /// Edits an existing customer based on their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be edited.</param>
        /// <param name="cust">The updated Customer2 entity.</param>
        /// <returns>The updated Customer2 entity.</returns>
        public Customer2 EditCustomer(int id, Customer2 cust)
        {
            if (id != null)
            {
                // Find the existing customer with the given ID in the list
                Customer2 existingCust = Customer2.lstCustomers2.FirstOrDefault(c => c.Id == id);

                if (existingCust != null)
                {
                    // Update the existing customer data
                    existingCust.PhoneNumber = cust.PhoneNumber;
                    existingCust.Name = cust.Name;
                    existingCust.Gender = cust.Gender;

                    return existingCust;
                }
            }

            return null;
        }

        /// <summary>
        /// Deletes a customer based on their unique identifier for version 2.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A message indicating the successful deletion.</returns>
        public string DeleteCustomer(int id)
        {
            if (id != null)
            {
                // Remove the customer with the given ID from the list
                Customer2.lstCustomers2.Remove(Customer2.lstCustomers2.FirstOrDefault(c => c.Id == id));

                // Return a deletion message
                return $"Customer With id {id} Deleted !!!";
            }

            return "Invalid Data";
        }
    }
}
