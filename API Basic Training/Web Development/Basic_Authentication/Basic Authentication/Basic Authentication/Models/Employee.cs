namespace Basic_Authentication.Models
{
    /// <summary>
    /// Represents an employee entity.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the city where the employee resides.
        /// </summary>
        public string City { get; set; }
    }
}
