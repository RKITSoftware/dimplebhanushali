namespace Resume_Builder.Helpers
{
    /// <summary>
    /// Enum defining messages for different operations.
    /// </summary>
    public enum EnumMessage
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
        public static string GetMessage(this EnumMessage enumValue)
        {
            switch (enumValue)
            {
                case EnumMessage.I:
                    return "Record inserted successfully.";
                case EnumMessage.D:
                    return "Record deleted successfully.";
                case EnumMessage.U:
                    return "Record updated successfully.";
                default:
                    return "Unknown operation.";
            }
        }


    }
}
