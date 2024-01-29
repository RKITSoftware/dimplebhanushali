using System.Collections.Generic;

namespace Dynamic_Values
{
    /// <summary>
    /// Provides a controller for handling generic collections.
    /// </summary>
    public static class ListStorage
    {
        /// <summary>
        /// Gets or sets the list of integers.
        /// </summary>
        public static List<int> NumList { get; } = new List<int> { 11, 22 };

        /// <summary>
        /// Gets or sets the list of booleans.
        /// </summary>
        public static List<bool> BoolList { get; } = new List<bool> { true };

        /// <summary>
        /// Gets or sets the list of strings.
        /// </summary>
        public static List<string> StrList { get; } = new List<string> { "Val 1", "Val 2" };
    }
}