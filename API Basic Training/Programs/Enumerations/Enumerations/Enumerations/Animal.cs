using System;

namespace Enumerations
{
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
