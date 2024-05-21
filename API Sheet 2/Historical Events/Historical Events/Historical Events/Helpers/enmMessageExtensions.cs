namespace Historical_Events.Helpers
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
    /// Enum defining messages for different operations.
    /// </summary>
    public enum enmRoles
    {
        /// <summary>
        /// Admin.
        /// </summary>
        A,

        /// <summary>
        /// User.
        /// </summary>
        U,

    }
    public static class enmMessageExtensions
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