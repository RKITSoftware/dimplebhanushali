using System;

namespace Enumerations
{
    /// <summary>
    /// Enumeration type representing different types of animals.
    /// </summary>
    enum AnimalType
    {
        Lion,
        Elephant,
        Penguin,
        Giraffe,
        Monkey
    }

    /// <summary>
    /// Enumeration type representing different animal habitats.
    /// </summary>
    enum Habitat
    {
        Savannah,
        Jungle,
        Arctic
    }

    /// <summary>
    /// Main program class.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
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
    }

    /// <summary>
    /// Represents an animal with properties such as name, type, and habitat.
    /// </summary>
    class Animal
    {
        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the animal (e.g., Lion, Elephant).
        /// </summary>
        public AnimalType Type { get; set; }

        /// <summary>
        /// Gets or sets the habitat of the animal (e.g., Savannah, Jungle).
        /// </summary>
        public Habitat Habitat { get; set; }

        /// <summary>
        /// Constructor to initialize an instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="type">The type of the animal.</param>
        /// <param name="habitat">The habitat of the animal.</param>
        public Animal(string name, AnimalType type, Habitat habitat)
        {
            Name = name;
            Type = type;
            Habitat = habitat;
        }

        /// <summary>
        /// Display information about the animal.
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Habitat: {Habitat}");
            Console.WriteLine();
        }
    }
}
