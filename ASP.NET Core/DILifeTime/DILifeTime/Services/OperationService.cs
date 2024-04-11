using System;
using DILifeTime.Interfaces;

namespace DILifeTime.Services
{
    /// <summary>
    /// Service that generates operation IDs.
    /// </summary>
    public class OperationService : IScopedService, ISingletonService, ITransientService
    {
        private readonly Guid _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationService"/> class.
        /// </summary>
        public OperationService()
        {
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the operation ID associated with the service.
        /// </summary>
        /// <returns>The operation ID.</returns>
        public Guid GetOperationID()
        {
            return _id;
        }
    }
}
