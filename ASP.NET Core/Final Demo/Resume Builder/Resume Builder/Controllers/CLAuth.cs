using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLAuth : ControllerBase
    {
        #region Private Members

        /// <summary>
        /// Implemnts  IBLAuthHandler interface
        /// </summary>
        private IAuthService _objBLAuthHandler;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor Injection
        /// </summary>
        /// <param name="authentication"> IAuthentication </param>
        public CLAuth(IAuthService objBLAuthHandler)
        {
            _objBLAuthHandler = objBLAuthHandler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticates user credential
        /// </summary>
        /// <param name="objDTOLog01"> Login Model </param>
        /// <returns> jwt token => if credential is correct
        ///           else => Login Failed 
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public IActionResult Login([FromBody] DTOLOG01 objDTOLog01)
        {
            return Ok(_objBLAuthHandler.Login(objDTOLog01.Email, objDTOLog01.Password));
        }

        #endregion
    }
}

