using System;
using System.IO;

namespace Basic_File_operations
{
    /// <summary>
    /// Entry point for the program demonstrating basic file operations.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method where the program execution starts.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this program).</param>
        static void Main(string[] args)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Specify the file name
            string fileName = "example.txt";

            // Combine the directory and file name to create the full file path
            string filePath = Path.Combine(currentDirectory, fileName);

            // Create an instance of FileOperations
            FileOperations fileHandler = new FileOperations();

            // Check if the file exists
            if (File.Exists(filePath))
            {
                Console.WriteLine("File exists.");

                // Read and display the content of the file
                fileHandler.ReadFile(filePath);

                // Append additional content to the file
                fileHandler.AppendToFile(filePath, "Additional content");
                Console.WriteLine("Content appended to the file.");

                // Read and display the updated content
                fileHandler.ReadFile(filePath);

                // Delete the file
                fileHandler.DeleteFile(filePath);
                Console.WriteLine("File deleted.");
            }
            else
            {
                Console.WriteLine("File does not exist. Creating...");

                // Create a new file and write some content
                fileHandler.CreateAndWriteFile(filePath);
                Console.WriteLine("File created and written.");
            }

            // Get File Info
            fileHandler.GetFileInfo(filePath);

            // Get Directory Info
            fileHandler.GetDirectoryInfo(currentDirectory);

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
