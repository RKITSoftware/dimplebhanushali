using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace JWT.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("api/token")]
        public Object GetToken()
        {
            string key = "my_secret_key_12345678901234567890123456789012";
            var issuer = "http://localhost:58548/";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "Dimple"));

            var token = new JwtSecurityToken(
                        issuer, issuer, permClaims, expires: DateTime.Now.AddDays(1),
                        signingCredentials: credentials
                );

            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new { data = jwt_token};
        }

        [HttpPost]
        [Route("api/getname")]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/getname2")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };
            }
            return null;
        }
    }
}