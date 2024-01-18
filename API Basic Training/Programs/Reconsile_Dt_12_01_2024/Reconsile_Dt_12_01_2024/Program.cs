using System;

namespace Reconsile_Dt_12_01_2024
{
    /// <summary>
    /// Represents the main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments (unused in this example).</param>
        static void Main(string[] args)
        {
            // Create an instance of OptionalParams
            OptionalParams objOptional = new OptionalParams();

            // Call the first method with specific values for both parameters
            objOptional.DisplayInfo("Dimple", 23);

            // Call the second method with specific values for both parameters
            objOptional.DisplayInfo(2, "UK");

            // Create an instance of BaseClass
            BaseClass baseObj = new BaseClass();

            // Call the Display method of BaseClass
            baseObj.Display();

            // Create an instance of DerivedClass
            DerivedClass objDerived = new DerivedClass();

            // Call the Display method of DerivedClass, which hides the Display method in BaseClass
            objDerived.Display();

            // Create an instance of NullableVariables
            NullableVariables objNullableDemo = new NullableVariables();

            // Call examples from NullableVariables
            objNullableDemo.Example1();
            objNullableDemo.Example2();

            Console.ReadKey();
        }
    }
}
