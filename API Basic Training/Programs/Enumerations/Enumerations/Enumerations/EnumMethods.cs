using System;

namespace Enumerations
{
    /// <summary>
    /// Business Logic class for performing enum operations.
    /// </summary>
    public class EnumMethods
    {
        /// <summary>
        /// Perform enum methods.
        /// </summary>
        public static void PerformEnumMethods()
        {
            // Define two instances of the AnimalType enum
            AnimalType lion = AnimalType.Lion;
            AnimalType elephant = AnimalType.Elephant;

            // Compare two enum values using the CompareTo method
            Console.WriteLine($"Comparison Result: {lion.CompareTo(elephant)}");

            // Format enum values using the Enum.Format method
            Console.WriteLine($"Formatted Lion: {Enum.Format(typeof(AnimalType), lion, "G")}");
            Console.WriteLine($"Formatted Elephant: {Enum.Format(typeof(AnimalType), elephant, "D")}");

            // Get hash code for enum values using the GetHashCode method
            Console.WriteLine($"Hash Code Lion: {lion.GetHashCode()}");
            Console.WriteLine($"Hash Code Elephant: {elephant.GetHashCode()}");

            // Get name of enum values using the Enum.GetName method
            Console.WriteLine($"Name Lion: {Enum.GetName(typeof(AnimalType), lion)}");
            Console.WriteLine($"Name Elephant: {Enum.GetName(typeof(AnimalType), elephant)}");

            // Get names of all values in the AnimalType enum using the Enum.GetNames method
            Console.WriteLine("AnimalType Enum Values:");
            foreach (string name in Enum.GetNames(typeof(AnimalType)))
            {
                Console.WriteLine(name);
            }

            // Get values of all constants in the AnimalType enum using the Enum.GetValues method
            Console.WriteLine("AnimalType Enum Values (as integers):");
            foreach (int value in Enum.GetValues(typeof(AnimalType)))
            {
                Console.WriteLine(value);
            }

            // Check if a specific value is defined in the AnimalType enum using the Enum.IsDefined method
            Console.WriteLine($"Is Lion defined in AnimalType enum? {Enum.IsDefined(typeof(AnimalType), "Lion")}");

            // Parse a string to an enum using the Enum.TryParse method
            AnimalType parsedType;
            if (Enum.TryParse("Elephant", out parsedType))
            {
                Console.WriteLine($"Parsed Enum Value: {parsedType}");
            }
            else
            {
                Console.WriteLine("Failed to parse enum value.");
            }
        }
    }
}
