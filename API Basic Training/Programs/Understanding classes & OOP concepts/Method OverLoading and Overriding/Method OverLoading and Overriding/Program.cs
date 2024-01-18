using System;

namespace Method_OverLoading_and_Overriding
{
    public class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Create an instance of NewClass
            NewClass objNewClass = new NewClass();

            // Call the Display method without parameters
            objNewClass.Display();

            // Call the Display method with a string parameter
            objNewClass.Display("RKIT");

            // Call the Display method with named parameters
            objNewClass.Display(prefix: "Welcome ", msg: "to India !!!");

            // Wait for a key press before exiting
            Console.ReadKey();
        }
    }
}
