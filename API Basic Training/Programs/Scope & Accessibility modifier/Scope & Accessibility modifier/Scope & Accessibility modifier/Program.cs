using System;

namespace Scope_And_Accessibility_Modifiers
{
    /// <summary>
    /// A program demonstrating the us_age of scope and accessibility modifiers.
    /// </summary>
    public class Program : ProtectedMod
    {
        /// <summary>
        /// A nested class demonstrating access modifiers.
        /// </summary>
        class AccessModifiers
        {
            public int num;
            // private AccessModifiers can only be used in the same class
            private int _age;

            /// <summary>
            /// Default constructor.
            /// </summary>
            public AccessModifiers()
            { }

            /// <summary>
            /// Parameterized constructor for private member.
            /// </summary>
            /// <param name="input_age">_age value to set.</param>
            public AccessModifiers(int inputAge)
            {
                _age = inputAge;
                Console.WriteLine($"Private _age => {_age}");
            }
        }

        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            AccessModifiers modifiers = new AccessModifiers();
            modifiers.num = 11;
            Console.WriteLine($"Public Modifier => {modifiers.num}");

            AccessModifiers mods = new AccessModifiers(23);

            Program program = new Program();
            program.num2 = 22;
            Console.WriteLine($"Protected Number => {program.num2}");

            Console.ReadKey();
        }
    }

    /// <summary>
    /// A class with a protected member.
        /// </summary>
    class ProtectedMod
    {
        protected int num2;
    }
}
