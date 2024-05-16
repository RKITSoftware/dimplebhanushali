using JWT.BL;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

namespace JWT.Controllers
{
    /// <summary>
    /// Controller for handling JWT token generation and user information retrieval.
    /// </summary>
    public class CLJwtController : ApiController
    {
        private readonly BLJWT _jwtBL;

        /// <summary>
        /// Constructor for Setting JWTBl Instance.
        /// </summary>
        public CLJwtController()
        {
            _jwtBL = new BLJWT();
        }

        /// <summary>
        /// Generates a JWT token with specific claims.
        /// </summary>
        /// <returns>An object containing the generated JWT token.</returns>
        [HttpGet]
        [Route("api/token")]
        public Object GetToken()
        {
            return _jwtBL.GenerateToken();
        }

        /// <summary>
        /// Retrieves the name from the authenticated user's claims.
        /// </summary>
        /// <returns>The name of the authenticated user if available; otherwise, "Invalid".</returns>
        [HttpPost]
        [Route("api/getname")]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                return _jwtBL.GetName1(identity);
            }
            else
            {
                return "Invalid";
            }
        }

        /// <summary>
        /// Retrieves the name from the authenticated user's claims using authorization.
        /// </summary>
        /// <returns>An object containing the name of the authenticated user if available; otherwise, null.</returns>
        [Authorize]
        [HttpPost]
        [Route("api/getname2")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                return _jwtBL.GetName2(claims);
            }
            return null;
        }
    }
}
