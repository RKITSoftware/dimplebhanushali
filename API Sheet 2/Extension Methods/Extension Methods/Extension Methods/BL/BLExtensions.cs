namespace ExtensionMethod
{
    /// <summary>
    /// Helper class containing extension methods.
    /// </summary>
    public static class BLHelper
    {
        /// <summary>
        /// Inverts the case of the first letter in a string.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>A string with the case of the first letter inverted.</returns>
        public static string InvertFirstLetterCase(this string inputString)
        {
            if (inputString.Length > 0)
            {
                char[] charArray = inputString.ToCharArray();
                charArray[0] = char.IsUpper(charArray[0]) ? char.ToLower(charArray[0]) : char.ToUpper(charArray[0]);
                return new string(charArray);
            }
            return inputString;
        }

        /// <summary>
        /// Checks if an integer value is greater than or equal to 1000.
        /// </summary>
        /// <param name="value">The input integer value.</param>
        /// <returns>True if the value is greater than or equal to 1000; otherwise, false.</returns>
        public static bool IsGreaterThanOrEqualTo(this int value)
        {
            return value >= 1000;
        }

    }
}
