using System;

namespace Static_Class.Models
{
    /// <summary>
    /// Static class for generating unique IDs.
    /// </summary>
    public static class UniqueId
    {
        /// <summary>
        /// Generates a new unique identifier.
        /// </summary>
        /// <returns>A string representing the newly generated unique ID.</returns>
        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
