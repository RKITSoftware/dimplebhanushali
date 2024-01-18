using System;

// Understanding Classes and OOP
namespace Classes_OOP
{
    /// <summary>
    /// Class to demonstrate various class members and OOP concepts.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Calling a static method from the class (does not require an instance)
            NewClass.Display();

            // Creating instances of the NewClass
            NewClass newClass = new NewClass();
            NewClass newClass1 = new NewClass();

            // Waiting for a key press before closing the console window
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Class to demonstrate properties, constructors, and methods.
    /// </summary>
    class NewClass
    {
        /// <summary>
        /// Gets or sets the Name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Instance Constructor. Called when an instance of NewClass is created.
        /// </summary>
        public NewClass()
        {
            Console.WriteLine("Instance Constructor");
        }

        /// <summary>
        /// Static (class) Constructor. Called once when the class is first accessed.
        /// </summary>
        static NewClass()
        {
            Console.WriteLine("Class Constructor");
        }

        /// <summary>
        /// Instance Method. Associated with instances of the class.
        /// </summary>
        public void Show()
        {
            Console.WriteLine("Hello World !!!");
        }

        /// <summary>
        /// Static (class) Method. Associated with the class itself.
        /// </summary>
        public static void Display()
        {
            Console.WriteLine("Hello World From Class Method!!!");
        }
    }
}
