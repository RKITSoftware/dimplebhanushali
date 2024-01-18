namespace Generics.Models
{
    /// <summary>
    /// Represents a person entity with basic information.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the city where the person resides.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// </summary>
        public long PhoneNumber { get; set; }
    }
}
