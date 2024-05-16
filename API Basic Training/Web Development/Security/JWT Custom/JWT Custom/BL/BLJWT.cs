using JWT_Custom.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace JWT_Custom.BL
{
    /// <summary>
    /// Class For Handling JWT Token
    /// </summary>
    public class BLJWT
    {
        // The secret key used for signing and validating JWT tokens
        private const string secretKey = "thisissecuritykeyofcustomjwttokenaut";

        /// <summary>
        /// Generates a JWT token for the provided user.
        /// </summary>
        /// <param name="objUser">The user for whom the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        internal static string GenerateToken(User objUser) // Method to generate JWT token
        {
            string issuer = "CustomJWTBearerTokenAPI"; // Defining the issuer of the token

            // Creating SymmetricSecurityKey and SigningCredentials
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)); // Generating symmetric key for token
            SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256); // Creating signing credentials with HMAC-SHA-256 algorithm

            // Creating claims for the user
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, objUser.Name), // Adding user name as a claim
                new Claim("Id", objUser.Id.ToString()) // Adding user ID as a claim
            };

            // Creating JWT token with claims and signing credentials
            JwtSecurityToken token = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time set to 1 hour from current time
                signingCredentials: credentials); // Setting signing credentials for the token

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); // Creating JWT token handler
            return handler.WriteToken(token); // Writing the JWT token as a string
        }

        /// <summary>
        /// Gets the IPrincipal object from the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>The IPrincipal object representing the user.</returns>
        internal static IPrincipal GetPrincipal(string token) // Method to retrieve user information from JWT token
        {
            // Extracting and decoding the payload from the JWT token
            string jwtEncodePayload = token.Split('.')[1]; // Extracting the payload part of the token

            // In Base64 encoding, the length of the encoded string must be a multiple of 4.
            int mod = jwtEncodePayload.Length % 4;

            // This determines the amount of padding needed to make the length of the payload a multiple of 4.
            // If the modulus is greater than 0, it means padding is required.
            int padding = mod > 0 ? 4 - mod : 0;

            // This adds the necessary padding to the payload to ensure its length is a multiple of 4.
            jwtEncodePayload = jwtEncodePayload + new string('=', padding); 

            try
            {
                string decodedPayloadBytes = Encoding.UTF8.GetString(Convert.FromBase64String(jwtEncodePayload)); // Decoding payload from Base64
                JObject json = JObject.Parse(decodedPayloadBytes); // Parsing the decoded payload as JSON

                // Retrieving user information based on the decoded payload
                User user = BLUser.GetUser(int.Parse(json["Id"].ToString())); // Retrieving user information

                // Creating GenericIdentity and GenericPrincipal objects for the user
                // represents the identity of a user in a generic manner. It typically contains the name of the user.
                GenericIdentity identity = new GenericIdentity(user.Name);

                // represents the role-based security context for a user in a generic manner.
                // It associates a set of roles with a user identity.
                IPrincipal principal = new GenericPrincipal(identity, user.Role.Split(','));

                return principal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); 
                return null; 
            }
        }

        /// <summary>
        /// Validates if the provided JWT token is valid and not expired.
        /// </summary>
        /// <param name="jwt">The JWT token to validate.</param>
        /// <returns>True if the token is valid and not expired, false otherwise.</returns>
        internal static bool IsJwtValid(string jwt) // Method to validate JWT token
        {
            // Splitting the JWT token into header, payload, and hash parts
            string[] jwtArray = jwt.Split('.'); // Splitting the token into its components
            string jwtHeader = jwtArray[0]; // Extracting the header part
            string jwtPayload = jwtArray[1]; // Extracting the payload part
            string jwtHash = jwtArray[2]; // Extracting the hash part

            string payload = jwtHeader + "." + jwtPayload; // Constructing the payload part

            // Calculating HMAC-SHA-256 for the header and payload
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)); // Creating HMACSHA256 object with secret key
            byte[] digest = hash.ComputeHash(Encoding.UTF8.GetBytes(payload)); // Computing hash of the payload

            // This converts the computed hash (in byte array form) into a Base64-encoded string representation.
            string digestBase64 = Convert.ToBase64String(digest)
                .Replace('+', '-') // Handling URL-safe Base64 characters
                .Replace('/', '_') // Handling URL-safe Base64 characters
                .Replace("=", ""); // Removing padding from Base64 string

            // If JWT hash matches the calculated hash, check for expiry time
            if (jwtHash.Equals(digestBase64)) // Comparing hashes
            {
                // If not expired, return true; otherwise, return false

                // Getting current time
                TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)); // Calculating current time
                long currTotalSecond = (long)span.TotalSeconds; // Converting current time to total seconds

                // Decoding jwtPayload from Base64
                int mod = jwtPayload.Length % 4; // Calculating padding length
                int padding = mod > 0 ? 4 - mod : 0; // Determining padding
                string paddedJwtPayload = jwtPayload + new string('=', padding); // Adding padding to payload
                byte[] encodedData = Convert.FromBase64String(paddedJwtPayload); // Converting payload to bytes
                string decodedData = Encoding.UTF8.GetString(encodedData); // Decoding bytes to UTF-8 string

                // Deserializing the jwtPayload decoded string
                var jwtPayloadObj = JwtPayload.Deserialize(decodedData); // Deserializing payload

                // Getting exp (expiry) claim
                object expiryTotalSecond; // Variable to hold expiry claim
                jwtPayloadObj.TryGetValue("exp", out expiryTotalSecond); // Retrieving expiry claim

                long exp = (long)expiryTotalSecond; // Converting expiry claim to total seconds

                // Comparing expiry and current time
                if (exp >= currTotalSecond) // Checking if token is expired
                {
                    return true; // Returning true if token is not expired
                }
            }
            return false; // Returning false if token is expired or hash doesn't match
        }
    }
}
