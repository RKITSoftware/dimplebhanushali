using System;

namespace Method_OverLoading_and_Overriding
{
    /// <summary>
    /// Example class inheriting from MyClass and overriding the Display method.
    /// </summary>
    public class NewClass : MyClass
    {
        /// <summary>
        /// Overrides the Display method from the base class (MyClass).
        /// </summary>
        public new void Display()
        {
            Console.WriteLine("This Method is Overridden !!!");
        }
    }
}
