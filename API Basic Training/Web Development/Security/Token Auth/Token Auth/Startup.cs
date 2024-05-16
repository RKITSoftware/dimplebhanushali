using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using Token_Auth.Provider;

[assembly: OwinStartup(typeof(Token_Auth.Startup))]

namespace Token_Auth
{
    /// <summary>
    /// Startup class for configuring OWIN pipeline and OAuth authentication.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration method called during application startup to set up OWIN pipeline.
        /// </summary>
        /// <param name="app">The OWIN application builder.</param>
        public void Configuration(IAppBuilder app)
        {
            // Enable Cross-Origin Resource Sharing (CORS)
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure OAuth options for the authorization server
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                // Allow the server to run without HTTPS during development
                AllowInsecureHttp = true,

                // Set the token endpoint URL
                TokenEndpointPath = new PathString("/login"),

                // Set the expiration time for the access token
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),

                // Set the custom authorization server provider
                Provider = new AuthorisationServerProvider(),
            };

            // Use the OAuth authorization server middleware
            app.UseOAuthAuthorizationServer(options);

            // Use the OAuth bearer authentication middleware
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Configure Web API routes and settings
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
