using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;

[assembly: OwinStartup(typeof(JWT.Startup))]

namespace JWT
{
    /// <summary>
    /// Configuration startup class for JWT authentication.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the application to use JWT Bearer Authentication.
        /// </summary>
        /// <param name="app">The OWIN application builder.</param>
        public void Configuration(IAppBuilder app)
        {
            // Configuring the application to use JWT Bearer Authentication
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    // Specifies the authentication mode
                    AuthenticationMode = AuthenticationMode.Active,

                    // Configuring token validation parameters
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        // Validate the issuer of the token
                        ValidateIssuer = true,

                        // Validate the audience of the token
                        ValidateAudience = true,

                        // Validate the signing key of the token
                        ValidateIssuerSigningKey = true,

                        // The issuer that should be considered valid
                        ValidIssuer = "http://localhost:58548/", // some string, normally web url

                        // The audience that should be considered valid
                        ValidAudience = "http://localhost:58548/",

                        // The key material that is used to sign the security token
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_12345678901234567890123456789012"))
                    }
                });
        }
    }
}
