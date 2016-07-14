using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(YunOffice.UserCenter.UI.Admin.App_Start.OwinStartup))]

namespace YunOffice.UserCenter.UI.Admin.App_Start
{
    public partial class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Admin/Account/Login"),
            //    CookieSecure = CookieSecureOption.SameAsRequest,
            //    ExpireTimeSpan = System.TimeSpan.FromMinutes(120),
            //});
        }
    }
}
