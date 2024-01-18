using System;
using System.IO;

namespace Basic_File_operations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Requesting the user to enter the name of the file (without extension)
            Console.Write("Enter the name of the file (without extension): => ");
            string fileName = Console.ReadLine();

            // Add ".txt" extension to the file name
            string filePath = $"{fileName}.txt";

            // Requesting the user to enter the text to be written to the file
            Console.Write($"Enter the text to be written to '{filePath}': => \n");
            string fileContent = Console.ReadLine();

            try
            {
                // Write text to the file
                File.WriteAllText(filePath, fileContent);

                // Notify the user about the successful write operation
                Console.WriteLine($"Text successfully written to '{filePath}'.");

                // Ask the user if they want to download the file
                Console.WriteLine("Do you want to download the file? (yes/no):");
                string downloadOption = Console.ReadLine().ToLower();

                if (downloadOption == "yes" || downloadOption == "y")
                {
                    // Read the file content and print it to the console
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine($"\nFile Content:\n{content}");

                    // Provide an option to download the file
                    Console.WriteLine($"\nDownload link: {Path.GetFullPath(filePath)}");
                }
                else
                {
                    Console.WriteLine("File not downloaded. Exiting...");
                }
            }
            catch (Exception ex)
            {
                // Handle and display any exceptions that may occur
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
