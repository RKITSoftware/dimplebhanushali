using Historical_Events.BL;
using Historical_Events.Data;
using Historical_Events.Models;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Historical_Events.User_Validation
{
    /// <summary>
    /// Business logic class for managing Historical Events.
    /// </summary>
    public class BLValidateUser
    {
        #region Public Methods

        /// <summary>
        /// Validates user login credentials.
        /// </summary>
        /// <param name="username">The username to be validated.</param>
        /// <param name="encryptedPassword">The encrypted password to be validated.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public bool IsLogin(string username, string password)
        {
            BLAES _objBlAes = new BLAES();
            password = _objBlAes.Encrypt(password);
            // Check if there is any user with the provided username and encrypted password

            bool isCredentialCorrect;
            using (IDbConnection db = MyDbContext.CreateConnection())
            {
                isCredentialCorrect = db.Exists<USR01>(usr => usr.r01f03 == username && usr.r01f06 == password);
            }

            return isCredentialCorrect;
        }

        /// <summary>
        /// GEnerates JWT Token for User Logged in.
        /// </summary>
        /// <param name="username">username.</param>
        /// <returns>String of JWT Token.</returns>
        public string GenerateJwtToken(string username, int userId, string role)
        {
            // Define token parameters
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("SecretKeyOfHistoricalEventsForJwtToken");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("Id", userId.ToString()), // Add user ID claim
                    new Claim(ClaimTypes.Role, role) // Add user ID claim
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create and serialize the token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// Validate token & if token is validated then add claims to current user 
        /// </summary>
        /// <param name="jwtToken"> jwt token </param>
        /// <returns></returns>
        public bool ValidateJwtToken(string jwtToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("SecretKeyOfHistoricalEventsForJwtToken");

            // Token validation parameters
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false, 
                ValidateLifetime = true, 
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                // Validate token and extract claims
                SecurityToken validatedToken;
                
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);
                
                // Set claims in current context
                HttpContext.Current.User = claimsPrincipal;
                
                return true; // Token is valid
            }
            catch (SecurityTokenException)
            {
                // Token validation failed
                return false;
            }
        }

        #endregion
    }
}
 