using loan_and_sell_be.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace loan_and_sell_be.Security
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IuserService _userService;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock,
                IUserService userService
                ) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            if (!Request.Headers.ContainsKey("Authorization")) {
                return AuthenticateResult.Fail("No viene authorization en header.");
            }
            bool result = false;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credential = Encoding.UTF8.GetString(credentialBytes).Split( new[] { ':' }, 2 );
                var email = credential[0];
                var pass = credential[1];
                result = _userService.IsUser(email, pass);
                
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Ocurrio un error en " + ex.ToString());
            }

            if (!result) {
                return AuthenticateResult.Fail("Usuario o password invalidos.");
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "id"),
                new Claim(ClaimTypes.Name, "user"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
