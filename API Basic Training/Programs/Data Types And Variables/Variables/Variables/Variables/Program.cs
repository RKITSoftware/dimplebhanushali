using System;

namespace Variables
{
    class Program
    {
        // Static Value
        // Member Variable
        static int value;
        static void Main(string[] args)
        {
            // Variables

            // Initialize an integer variable 'date' with value 9.
            int date = 9; 
            
            // Declare a string variable 'month'.
            string month; 
            
            // Declare and initialize a constant integer variable 'year' with value 2001.
            const int year = 2001; 

            // Assign the value 22 to the static variable 'value'.
            value = 22; 
            
            // Update the value of the variable 'date' to 11.
            date = 11; 
            
            // Assign the value "January" to the variable 'month'.
            month = "January"; 

            // Display the date of birth.
            Console.WriteLine($"Date of Birth => {date} - {month} - {year}"); 
            
            // Display the static value.
            Console.WriteLine($"Static Value => {value}"); 

            // Wait for user input before closing the console window.
            Console.ReadKey(); 

        }
    }
}
