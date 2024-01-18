using System;

namespace Reconsile_Dt_12_01_2024
{
    /// <summary>
    /// Represents a class demonstrating nullable value types and the null coalescing operator.
    /// </summary>
    public class NullableVariables
    {
        /// <summary>
        /// Example 1: Nullable Value Types
        /// </summary>
        public void Example1()
        {
            // Nullable value type
            int? nullableInt = null;

            Console.WriteLine("Example 1: Nullable Value Types");
            Console.WriteLine($"Nullable Int: {nullableInt.GetValueOrDefault()}");
            Console.WriteLine($"Has Value? => {nullableInt.HasValue}");
            Console.WriteLine();
        }

        /// <summary>
        /// Example 2: Null Coalescing Operator
        /// </summary>
        public void Example2()
        {
            // Nullable value type
            int? nullableInt = null;

            // Null coalescing operator
            int result = nullableInt ?? 100;

            Console.WriteLine("Example 2: Null Coalescing Operator");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();
        }
    }
}
