using DILifeTime.Interfaces;

namespace DILifeTime.BL.Services
{
    /// <summary>
    /// Service that generates operation IDs.
    /// </summary>
    public class OperationService : IScopedService, ISingletonService, ITransientService
    {
        #region Private Member
        /// <summary>
        /// GUID 
        /// </summary>
        private readonly Guid _id;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationService"/> class.
        /// </summary>
        public OperationService()
        {
            _id = Guid.NewGuid();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Gets the operation ID associated with the service.
        /// </summary>
        /// <returns>The operation ID.</returns>
        public Guid GetOperationID()
        {
            return _id;
        }
        #endregion
    }
}
