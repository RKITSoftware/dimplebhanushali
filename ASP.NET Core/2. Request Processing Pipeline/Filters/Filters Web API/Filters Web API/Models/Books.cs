namespace Filters_Web_API.MAL
{
    /// <summary>
    /// Represents a book with basic information such as ID, title, author, and publication year.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }
    }

}
