using System;

namespace Method_OverLoading_and_Overriding
{
    /// <summary>
    /// Example class demonstrating method overloading in C#
    /// </summary>
    public class MyClass
    {
        /// <summary>
        /// Display method with no parameters.
        /// </summary>
        public virtual void Display()
        {
            Console.WriteLine("Good Morning !!!");
        }

        /// <summary>
        /// Display method with two parameters and an optional default prefix.
        /// </summary>
        /// <param name="msg">The message to be displayed.</param>
        /// <param name="prefix">Optional prefix for the message (default is "Good Morning ").</param>
        public void Display(string msg, string prefix = "Good Morning ")
        {
            Console.WriteLine(prefix + msg);
        }
    }
}
