using System;

namespace Reconsile_Dt_12_01_2024
{
    /// <summary>
    /// Represents a base class with a private message and a display method.
    /// </summary>
    class BaseClass
    {
        // Private field
        private string msg = "This is Base Class";

        /// <summary>
        /// Property to access the private field.
        /// </summary>
        protected string Message
        {
            get { return msg; }
        }

        /// <summary>
        /// Displays the message of the base class.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"BaseClass Display: {Message}");
        }
    }

    /// <summary>
    /// Represents a derived class inheriting from the BaseClass with a new display method.
    /// </summary>
    class DerivedClass : BaseClass
    {
        /// <summary>
        /// Method hiding using the 'new' keyword to display the message of the derived class.
        /// </summary>
        public new void Display()
        {
            Console.WriteLine($"DerivedClass Display: {Message}");
        }
    }
}
