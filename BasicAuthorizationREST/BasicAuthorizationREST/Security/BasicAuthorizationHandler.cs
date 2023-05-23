using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BasicAuthorizationREST.Security
{
    public class BasicAuthorizationHandler : AuthenticationHandler<BasicAuthorizationOption>
    {
        public BasicAuthorizationHandler(IOptionsMonitor<BasicAuthorizationOption> options,
                                         ILoggerFactory logger,
                                         UrlEncoder encoder,
                                         ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //1. gelen request header içinde: Authorization var mı?
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }


            //2. Authorization değeri doğru formatta mı?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }
            //3. Authorization'un şeması Basic mi?
            if (!headerValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //'admin:123456'

            var userNameAndPassword = Convert.FromBase64String(headerValue.Parameter);
            string original = Encoding.UTF8.GetString(userNameAndPassword);
            var userName = original.Split(':')[0];
            var password = original.Split(':')[1];

            if (userName == "admin" && password == "123456")
            {
                Claim[] claims = new Claim[] { new Claim(ClaimTypes.Name, "admin") };
                ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));

            }
            return Task.FromResult(AuthenticateResult.Fail("Hatalı kullanıcı adı veya şifre"));


        }
    }
}
