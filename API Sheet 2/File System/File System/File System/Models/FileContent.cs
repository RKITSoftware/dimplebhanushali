namespace File_System.Models
{
    /// <summary>
    /// Represents the content and name of a file.
    /// </summary>
    public class FileContent
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        public string Content { get; set; }
    }
}
