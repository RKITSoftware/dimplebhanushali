namespace API.Models
{
    /// <summary>
    /// Represents a student entity with basic information.
    /// </summary>
    public class Students
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the course in which the student is enrolled.
        /// </summary>
        public string Course { get; set; }

        /// <summary>
        /// Gets or sets the semester in which the student is currently studying.
        /// </summary>
        public int Sem { get; set; }
    }
}
