namespace Virtual_Diary.Helper
{
    /// <summary>
    /// Enum defining messages for different operations.
    /// </summary>
    public enum enmOperations
    {
        /// <summary>
        /// Record inserted successfully.
        /// </summary>
        I,
        /// <summary>
        /// Record deleted successfully.
        /// </summary>
        D,
        /// <summary>
        /// Record updated successfully.
        /// </summary>
        U
    }

    /// <summary>
    /// Extension methods for EnumMessage enum.
    /// </summary>
    public static class enmMessageExtensions
    {
        /// <summary>
        /// Gets the corresponding message for the enum value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The message corresponding to the enum value.</returns>
        public static string GetMessage(this enmOperations enumValue)
        {
            switch (enumValue)
            {
                case enmOperations.I:
                    return "Record inserted successfully.";
                case enmOperations.D:
                    return "Record deleted successfully.";
                case enmOperations.U:
                    return "Record updated successfully.";
                default:
                    return "Unknown operation.";
            }
        }
    }
}