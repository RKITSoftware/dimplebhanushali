using System;

namespace DILifeTime.Interfaces
{
    /// <summary>
    /// Interface representing a scoped service.
    /// </summary>
    public interface IScopedService
    {
        /// <summary>
        /// Gets the operation ID associated with the scoped service.
        /// </summary>
        /// <returns>The operation ID.</returns>
        Guid GetOperationID();
    }
}
