using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Token_Auth.Provider
{
    /// <summary>
    /// Custom OAuth authorization server provider for handling client authentication and user credentials validation.
    /// </summary>
    public class AuthorisationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Validates the client's authentication during the OAuth token request.
        /// </summary>
        /// <param name="context">The context containing information about the client authentication.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Always validate the client
            context.Validated();
        }

        /// <summary>
        /// Grants the resource owner credentials by validating the user's credentials.
        /// </summary>
        /// <param name="context">The context containing information about the resource owner credentials.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserRepo objUserRepo = new UserRepo())
            {
                // Validate user credentials
                var user = objUserRepo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    // Set an error if user validation fails
                    context.SetError("Invalid Grant", "UserName or Password is Incorrect");
                    return;
                }

                // Create a claims identity with user information
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));

                // Add user roles as claims
                foreach (var role in user.Roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString().Trim()));
                }

                // Validate and set the claims identity
                context.Validated(identity);
            }
        }
    }
}
