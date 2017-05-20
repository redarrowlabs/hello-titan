using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(HelloWorld.Startup))]
namespace HelloWorld
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
                })
                .UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,

                    ClientId = "0620bd8e-2790-4e68-bff7-232e77701866",
                    Authority = "https://test.redarrow.io/auth",
                    RedirectUri = "http://localhost:8080/auth-callback",

                    ResponseType = "code id_token token",
                    Scope = "openid profile email"
                });
        }
    }
}