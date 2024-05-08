using Resume_Builder.BL.Interfaces;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.Models;
using Resume_Builder.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Resume_Builder.BL.Services
{
    /// <summary>
    /// Implements interface of CLAuth handler
    /// </summary>
    public class BLAuthHandler : IAuthService
    {
        #region Private Members

        /// <summary>
        /// Implementation of cryptography
        /// </summary>
        private ICryptography _cryptography;

        /// <summary>
        /// Response of action method
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// OrmLite Connection Factory
        /// </summary>
        private Resume_Builder.Data.DbConnectionFactory _dbFactory;

        /// <summary>
        /// Authentication Service 
        /// </summary>
        private IAuthentication _authService;

        #endregion

        #region Constructor

        /// <summary>
        /// Configures necessary dependency injections
        /// </summary>
        public BLAuthHandler(ICryptography cryptography, Resume_Builder.Data.DbConnectionFactory dbFactory, IAuthentication authentication)
        {
            _cryptography = cryptography;
            _dbFactory = dbFactory;
            _authService = authentication;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Login Method 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Response Login(string email, string password)
        {
            _objResponse = new Response();

            int userId;

            // Credential Verification & 
            // extracting information to be added in claims
            using (var db = _dbFactory.CreateConnection())
            {
                USR01 objUsr01 = db.Single<USR01>(usr => usr.R01F04 == email && usr.R01F07 == password);
                // Invalid Credential
                if (objUsr01 == null)
                {
                    _objResponse.HasError = true;
                    _objResponse.Message = "Login Failed";
                    return _objResponse;
                }
                userId = objUsr01.R01F01;
                //walletId = db.Single<Wlt01>(wlt => wlt.T01f02 == userId).T01f01;
            }

            string token = _authService.GenerateJwtToken(email, userId);

            _objResponse.Message = "Login Successful";
            _objResponse.Data = new { token = token };
            return _objResponse;
        }

        #endregion
    }
}
