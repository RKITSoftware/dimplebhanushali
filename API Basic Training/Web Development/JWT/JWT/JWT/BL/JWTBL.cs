using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWT.BL
{
    /// <summary>
    /// Business Logic class for handling JWT-related operations.
    /// </summary>
    public class JWTBL
    {
        /// <summary>
        /// Generates a JWT token with specific claims.
        /// </summary>
        /// <returns>An object containing the generated JWT token.</returns>
        public object GenerateToken()
        {
            // Secret key used for token signing and validation
            string key = "my_secret_key_12345678901234567890123456789012";
            var issuer = "http://localhost:58548/";

            // Creating a security key and credentials for token signing
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Adding claims to the token payload
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "Dimple"));

            // Creating a JWT token with the specified claims
            var token = new JwtSecurityToken(
                issuer, issuer, permClaims, expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            // Writing the token to a string
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            // Returning the JWT token in an object
            return new { data = jwt_token };
        }

        /// <summary>
        /// Retrieves the name from the authenticated user's claims.
        /// </summary>
        /// <returns>The name of the authenticated user if available; otherwise, "Invalid".</returns>
        public string GetName1(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
            }
            return "Valid";
        }

        /// <summary>
        /// Retrieves the name from the authenticated user's claims using authorization.
        /// </summary>
        /// <returns>An object containing the name of the authenticated user if available; otherwise, null.</returns>
        public object GetName2(IEnumerable<Claim> claims)
        {
            var name = claims?.Where(p => p.Type == "name").FirstOrDefault()?.Value;
            return new
            {
                data = name
            };
        }
    }
}
