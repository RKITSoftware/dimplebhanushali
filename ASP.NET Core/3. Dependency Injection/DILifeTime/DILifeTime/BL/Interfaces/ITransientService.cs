namespace DILifeTime.Interfaces
{
    /// <summary>
    /// Interface representing a transient service.
    /// </summary>
    public interface ITransientService
    {
        /// <summary>
        /// Gets the operation ID associated with the transient service.
        /// </summary>
        /// <returns>The operation ID.</returns>
        Guid GetOperationID();
    }
}
