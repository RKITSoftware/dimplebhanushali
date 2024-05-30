namespace DILifeTime.Interfaces
{
    /// <summary>
    /// Interface representing a singleton service.
    /// </summary>
    public interface ISingletonService
    {
        /// <summary>
        /// Gets the operation ID associated with the singleton service.
        /// </summary>
        /// <returns>The operation ID.</returns>
        Guid GetOperationID();
    }
}
