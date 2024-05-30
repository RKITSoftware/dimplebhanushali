namespace Resume_Builder.Helpers
{
    /// <summary>
    /// Enum defining messages for different operations.
    /// </summary>
    public enum enmOperation
    {
        /// <summary>
        /// Insert.
        /// </summary>
        I,

        /// <summary>
        /// Delete.
        /// </summary>
        D,

        /// <summary>
        /// Update.
        /// </summary>
        U,


    }
    /// <summary>
    /// Extension methods for EnumMessage enum.
    /// </summary>
    public static class EnumMessageExtensions
    {
        /// <summary>
        /// Gets the corresponding message for the enum value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The message corresponding to the enum value.</returns>
        public static string GetMessage(this enmOperation enumValue)
        {
            switch (enumValue)
            {
                case enmOperation.I:
                    return "Record inserted successfully.";
                case enmOperation.D:
                    return "Record deleted successfully.";
                case enmOperation.U:
                    return "Record updated successfully.";
                default:
                    return "Unknown operation.";
            }
        }
    }
}
