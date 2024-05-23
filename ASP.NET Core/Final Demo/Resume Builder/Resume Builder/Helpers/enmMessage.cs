namespace Resume_Builder.Helpers
{
    /// <summary>
    /// Enum defining messages for different operations.
    /// </summary>
    public enum enmMessage
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
        public static string GetMessage(this enmMessage enumValue)
        {
            switch (enumValue)
            {
                case enmMessage.I:
                    return "Record inserted successfully.";
                case enmMessage.D:
                    return "Record deleted successfully.";
                case enmMessage.U:
                    return "Record updated successfully.";
                default:
                    return "Unknown operation.";
            }
        }


    }
}
