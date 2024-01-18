using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Token_Auth;

namespace Token_Auth.Provider
{
    public class AuthorisationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserRepo objUserRepo = new UserRepo())
            {
                var user = objUserRepo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("Invalid Grant", "UserName or Password is Incorrect");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));

                foreach (var role in user.Roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString().Trim()));
                }

                context.Validated(identity);
            }
        }
    }

}