using System;
using System.Diagnostics;

namespace Reconsile_Dt_10_1_24
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Decimal.Parse
            // Convert integer to decimal using Convert.ToDecimal
            int num = 11;
            decimal dec = Convert.ToDecimal(num);

            // Print the result
            Console.WriteLine(dec);


            #endregion

            #region Object Type
            // Create an object holding a string value "RKIT"
            object name = "RKIT";

            // Convert the object to a string using ToString method
            string a1 = name.ToString();


            #endregion

            #region Dynamic Type
            // Declare a dynamic variable 'val' and assign a string value "RKIT" to it
            dynamic val = "RKIT";

            // Assign the dynamic value to a strongly-typed string variable 'val2'
            string val2 = val;

            #endregion

            #region For and ForEach
            // Generate an array of names with 5,000,000 elements
            string[] names = GenerateNamesArray(5000000);

            // Measure time using 'for' loop
            Stopwatch forLoopStopwatch = Stopwatch.StartNew();
            ForLoop(names);
            forLoopStopwatch.Stop();

            // Measure time using 'foreach' loop
            Stopwatch foreachLoopStopwatch = Stopwatch.StartNew();
            ForeachLoop(names);
            foreachLoopStopwatch.Stop();

            // Output the time taken for each loop
            Console.WriteLine($"'for' loop took: {forLoopStopwatch.ElapsedMilliseconds} milliseconds");
            Console.WriteLine($"'foreach' loop took: {foreachLoopStopwatch.ElapsedMilliseconds} milliseconds");

            #endregion

            #region GetValue function
            // Declare an array of car names
            string[] cars = { "Audi", "BMW", "Mercedes", "Fortuner", "Lamborghini" };

            // Retrieve the value at index 2 using the GetValue method
            Console.WriteLine(cars.GetValue(2));

            #endregion

            #region Optional Parameter with optional Arguement

            // Create an instance of the Example class
            Example example = new Example();

            // Call the DisplayMessage method with all parameters provided
            example.DisplayMessage("Hello", 3, true);

            // Call the DisplayMessage method with only the required parameter and one optional parameter
            example.DisplayMessage("Hi", 2);

            // Call the DisplayMessage method with only the required parameter 
            // (both optional parameters will use their default values)
            example.DisplayMessage("Hey", isUppercase: true);

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// Iterates through an array using a 'for' loop, accessing each element by index.
        /// </summary>
        /// <param name="names">Array of strings to be iterated.</param>
        static void ForLoop(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {}
        }

        /// <summary>
        /// Iterates through an array using a 'foreach' loop, accessing each element directly.
        /// </summary>
        /// <param name="names">Array of strings to be iterated.</param>
        static void ForeachLoop(string[] names)
        {
            foreach (var name in names)
            {
                // Access 'name': Perform operations on each element of the array.
            }
        }


        /// <summary>
        /// Generates an array of strings with sequential names based on the provided count.
        /// </summary>
        /// <param name="count">Number of elements in the generated array.</param>
        /// <returns>An array of strings with sequential names.</returns>
        static string[] GenerateNamesArray(int count)
        {
            // Create a new string array with the specified count
            string[] names = new string[count];

            // Populate the array with sequential names ("Name0", "Name1", ..., "Name[count-1]")
            for (int i = 0; i < count; i++)
            {
                names[i] = "Name" + i.ToString();
            }

            // Return the generated array
            return names;
        }
    }

    /// <summary>
    /// Example class with methods demonstrating the use of optional parameters.
    /// </summary>
    public class Example
    {
        /// <summary>
        /// Displays a message a specified number of times.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="count">The number of times to display the message (optional, default is 1).</param>
        public void DisplayMessage(string message, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Displays a message a specified number of times with an option for uppercase.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="count">The number of times to display the message (optional, default is 1).</param>
        /// <param name="isUppercase">A flag indicating whether the message should be displayed in uppercase (optional, default is false).</param>
        public void DisplayMessage(string message, int count = 1, bool isUppercase = false)
        {
            for (int i = 0; i < count; i++)
            {
                if (isUppercase)
                {
                    Console.WriteLine(message.ToUpper());
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
