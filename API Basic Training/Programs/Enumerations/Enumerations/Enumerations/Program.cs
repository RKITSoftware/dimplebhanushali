using System;

namespace Enumerations
{
    enum AnimalType
    {
        Lion,
        Elephant,
        Penguin,
        Giraffe,
        Monkey
    }

    enum Habitat
    {
        Savannah,
        Jungle,
        Arctic
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example of Enum methods
            PerformEnumMethods();

            // Create an array of animals
            Animal[] zoo = new Animal[]
            {
                new Animal("Simba", AnimalType.Lion, Habitat.Savannah),
                new Animal("Dumbo", AnimalType.Elephant, Habitat.Jungle),
                new Animal("Skipper", AnimalType.Penguin, Habitat.Arctic),
                new Animal("Melman", AnimalType.Giraffe, Habitat.Savannah),
                new Animal("Curious George", AnimalType.Monkey, Habitat.Jungle)
            };

            // Display information about each animal in the zoo
            Console.WriteLine("Welcome to the Zoo!");

            foreach (var animal in zoo)
            {
                animal.DisplayInfo();
            }

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }

        static void PerformEnumMethods()
        {
            // Example of Enum methods
            AnimalType lion = AnimalType.Lion;
            AnimalType elephant = AnimalType.Elephant;

            // Compare two enum values
            Console.WriteLine($"Comparison Result: {lion.CompareTo(elephant)}");

            // Format enum values
            Console.WriteLine($"Formatted Lion: {Enum.Format(typeof(AnimalType), lion, "G")}");
            Console.WriteLine($"Formatted Elephant: {Enum.Format(typeof(AnimalType), elephant, "D")}");

            // Get hash code for enum values
            Console.WriteLine($"Hash Code Lion: {lion.GetHashCode()}");
            Console.WriteLine($"Hash Code Elephant: {elephant.GetHashCode()}");

            // Get name of enum values
            Console.WriteLine($"Name Lion: {Enum.GetName(typeof(AnimalType), lion)}");
            Console.WriteLine($"Name Elephant: {Enum.GetName(typeof(AnimalType), elephant)}");

            // Get names and values of AnimalType enum
            Console.WriteLine("AnimalType Enum Values:");
            foreach (string name in Enum.GetNames(typeof(AnimalType)))
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("AnimalType Enum Values (as integers):");
            foreach (int value in Enum.GetValues(typeof(AnimalType)))
            {
                Console.WriteLine(value);
            }

            // Check if a value exists in an enum
            Console.WriteLine($"Is Lion defined in AnimalType enum? {Enum.IsDefined(typeof(AnimalType), "Lion")}");

            // Parse string to enum
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
