namespace Static_Class
{
    /// <summary>
    /// Static utility class for mathematical operations.
    /// </summary>
    public class MathUtility
    {
        // Static field
        public static int Counter = 0;

        /// <summary>
        /// Adds two integers and increments the counter.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The sum of the two integers.</returns>
        public static int Add(int a, int b)
        {
            Counter++; // Increment the static field
            return a + b;
        }

        public static int MultiplyByTwo
        {
            get { return Counter * 2; }
        }
    }
}
