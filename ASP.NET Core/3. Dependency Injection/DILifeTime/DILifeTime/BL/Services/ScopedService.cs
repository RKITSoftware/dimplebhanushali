using DILifeTime.Interfaces;

namespace DILifeTime.BL.Services
{
    public class ScopedService : IScopedService
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
        public ScopedService()
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
