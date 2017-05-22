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

                    ClientId = "INSERT YOUR APP KEY HERE",
                    Authority = "https://sandbox.redarrow.io/auth",
                    RedirectUri = "http://localhost:8080/auth-callback",

                    ResponseType = "code id_token token",
                    Scope = "openid profile email",

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthorizationCodeReceived = async notification =>
                        {
                            var identity = notification.AuthenticationTicket.Identity;

                            var accessToken = notification.ProtocolMessage.AccessToken;

                            identity.AddClaim(new Claim("access_token", accessToken));

                            var userInfoClient =
                                new UserInfoClient("https://sandbox.redarrow.io/auth/connect/userinfo");
                            var userInfo = await userInfoClient.GetAsync(accessToken);

                            identity.AddClaims(userInfo.Claims);
                        }
                    }
                });
        }
    }
}