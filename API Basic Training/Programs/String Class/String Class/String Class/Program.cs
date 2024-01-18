using System;

namespace StringOperations
{
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // String Operations

            // Sample strings
            string str1 = "Hello, World!";
            string str2 = "   Trim Example   ";
            string str3 = "abcABC123";

            // GetType
            Type typeOfStr1 = str1.GetType();
            Console.WriteLine($"Type of str1: {typeOfStr1}");

            // GetHashCode
            int hashCode = str1.GetHashCode();
            Console.WriteLine($"HashCode of str1: {hashCode}");

            // LastIndexOf
            int lastIndexOfResult = str1.LastIndexOf('o');
            Console.WriteLine($"LastIndexOf 'o': {lastIndexOfResult}");

            // PadLeft
            string paddedString = str3.PadLeft(12, '*');
            Console.WriteLine($"Original: '{str3}', PaddedLeft: '{paddedString}'");

            // Compare
            int compareResult = string.Compare(str1, "Hello, Universe!");
            Console.WriteLine($"Compare Result: {compareResult}");

            // Contains
            bool containsResult = str1.Contains("World");
            Console.WriteLine($"Contains 'World': {containsResult}");

            // IndexOf
            int indexOfResult = str1.IndexOf("World");
            Console.WriteLine($"IndexOf 'World': {indexOfResult}");

            // Substring
            string substringResult = str1.Substring(7, 5);
            Console.WriteLine($"Substring: {substringResult}");

            // ToLower and ToUpper
            string lowerCase = str3.ToLower();
            string upperCase = str3.ToUpper();
            Console.WriteLine($"ToLower: {lowerCase}, ToUpper: {upperCase}");

            // Trim
            string trimmedString = str2.Trim();
            Console.WriteLine($"Original: '{str2}', Trimmed: '{trimmedString}'");

            // Replace
            string replacedString = str1.Replace("World", "Universe");
            Console.WriteLine($"Replace 'World' with 'Universe': {replacedString}");

            // Split
            string[] words = str1.Split(' ');
            Console.WriteLine("Split Result:");
            foreach (var word in words)
            {
                Console.WriteLine($"- {word}");
            }

            Console.ReadKey();
        }
    }
}
