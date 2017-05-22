using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
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

                    ClientId = "insert your application key here",
                    Authority = "https://sandbox.redarrow.io/auth",
                    RedirectUri = "http://localhost:8080/auth-callback",

                    ResponseType = "code id_token token",
                    Scope = "openid profile email",

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthorizationCodeReceived = notification =>
                        {
                            var identity = notification.AuthenticationTicket.Identity;

                            identity.AddClaim(new Claim("access_token", notification.ProtocolMessage.AccessToken));
                            identity.AddClaim(new Claim("expires_at", DateTime.UtcNow.AddSeconds(
                                    double.Parse(notification.ProtocolMessage.ExpiresIn))
                                .ToString("O")));
                            //identity.AddClaim(new Claim("refresh_token", notification.ProtocolMessage.RefreshToken));
                            identity.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));

                            return Task.FromResult(0);
                        }
                    }
                });
        }
    }
}