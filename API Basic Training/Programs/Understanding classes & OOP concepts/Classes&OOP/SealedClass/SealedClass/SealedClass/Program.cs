using System;

namespace SealedClass
{
    /// <summary>
    /// Base sealed class.
    /// </summary>
    sealed class SealedClass
    {
        /// <summary>
        /// Display a message from the sealed class.
        /// </summary>
        public void DisplayMessage()
        {
            Console.WriteLine("This is a sealed class.");
        }
    }

    /// <summary>
    /// Main program to demonstrate a sealed class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        static void Main()
        {
            // Create an instance of the sealed class
            SealedClass sealedObj = new SealedClass();
            sealedObj.DisplayMessage();

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
