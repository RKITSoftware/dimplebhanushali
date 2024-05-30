using Resume_Builder.Models;

namespace Resume_Builder.BL.Interfaces
{
    /// <summary>
    /// Authorization / Login interface 
    /// </summary>
    public interface IAuthService
    {

        #region Public Methods

        /// <summary>
        /// Login method which generates token for correct credential 
        /// </summary>
        /// <param name="email"> email id </param>
        /// <param name="password"> password </param>
        /// <returns> object of response </returns>
        Response Login(string email, string password);

        #endregion
    }
}