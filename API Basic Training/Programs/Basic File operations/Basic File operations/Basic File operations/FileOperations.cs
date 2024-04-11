using System;
using System.IO;

namespace Basic_File_operations
{
    /// <summary>
    /// Business Logic class for file handling operations.
    /// </summary>
    public class FileOperations
    {
        #region Public Methods
        /// <summary>
        /// Creates a new file at the specified path and writes some content to it.
        /// </summary>
        /// <param name="filePath">The path of the file to be created.</param>
        public void CreateAndWriteFile(string filePath)
        {
            // Using statement ensures that the FileStream is properly closed and disposed of
            using (FileStream fs = File.Create(filePath))
            {
                // Writing some content to the file
                byte[] content = new System.Text.UTF8Encoding(true).GetBytes("Hello, this is a file content.");
                fs.Write(content, 0, content.Length);
            }
        }

        /// <summary>
        /// Reads the content of the file at the specified path and displays it.
        /// </summary>
        /// <param name="filePath">The path of the file to be read.</param>
        public void ReadFile(string filePath)
        {
            // Reading the content of the file
            string content = File.ReadAllText(filePath);
            Console.WriteLine("File Content:\n" + content);
        }

        /// <summary>
        /// Appends additional content to the file at the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to which content is appended.</param>
        /// <param name="additionalContent">The content to be appended.</param>
        public void AppendToFile(string filePath, string additionalContent)
        {
            // Appending content to the file
            File.AppendAllText(filePath, "\n" + additionalContent);
        }

        /// <summary>
        /// Deletes the file at the specified path.
        /// </summary>
        /// <param name="filePath">The path of the file to be deleted.</param>
        public void DeleteFile(string filePath)
        {
            // Deleting the file
            File.Delete(filePath);
        }

        /// <summary>
        /// Displays information about the file at the specified path using FileInfo.
        /// </summary>
        /// <param name="filePath">The path of the file to get information about.</param>
        public void GetFileInfo(string filePath)
        {
            // Getting information about the file using FileInfo
            FileInfo fileInfo = new FileInfo(filePath);

            Console.WriteLine($"File Name: {fileInfo.Name}");
            Console.WriteLine($"File Size: {fileInfo.Length} bytes");
            Console.WriteLine($"Creation Time: {fileInfo.CreationTime}");
            Console.WriteLine($"Last Access Time: {fileInfo.LastAccessTime}");
            Console.WriteLine($"Last Write Time: {fileInfo.LastWriteTime}");
        }

        /// <summary>
        /// Displays information about the directory at the specified path using DirectoryInfo.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to get information about.</param>
        public void GetDirectoryInfo(string directoryPath)
        {
            // Getting information about the directory using DirectoryInfo
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            Console.WriteLine($"Directory Name: {directoryInfo.Name}");
            Console.WriteLine($"Number of Files: {directoryInfo.GetFiles().Length}");
            Console.WriteLine($"Number of Subdirectories: {directoryInfo.GetDirectories().Length}");
            Console.WriteLine($"Creation Time: {directoryInfo.CreationTime}");
            Console.WriteLine($"Last Access Time: {directoryInfo.LastAccessTime}");
            Console.WriteLine($"Last Write Time: {directoryInfo.LastWriteTime}");
        }
        #endregion
    }
}
