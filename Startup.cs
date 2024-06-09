using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Owin;
using MvcCodeFlowClientManual.Data;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartup(typeof(MvcCodeFlowClientManual.Startup))]

namespace MvcCodeFlowClientManual
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies"
                });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "TempState",
                AuthenticationMode = AuthenticationMode.Passive
            });

        }

    }
}