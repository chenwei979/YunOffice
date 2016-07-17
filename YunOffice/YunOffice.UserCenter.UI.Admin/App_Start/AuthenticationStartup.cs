using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

[assembly: OwinStartup(typeof(YunOffice.UserCenter.UI.Admin.App_Start.OwinStartup))]

namespace YunOffice.UserCenter.UI.Admin.App_Start
{
    public partial class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(AutofacWebRequestLifetimeScopeMiddleware));

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Admin/Account/Login"),
            //    CookieSecure = CookieSecureOption.SameAsRequest,
            //    ExpireTimeSpan = System.TimeSpan.FromMinutes(120),
            //});
        }
    }

    public class AutofacWebRequestLifetimeScopeMiddleware : OwinMiddleware
    {
        public AutofacWebRequestLifetimeScopeMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            using (var scope = Infrastructure.AutofacContainerBuilder.Singleton.GetRootInstance().BeginLifetimeScope("AutofacWebRequest"))
            {
                Infrastructure.AutofacContainerBuilder.Singleton.SetInstance(scope);
                await Next.Invoke(context);
                Infrastructure.AutofacContainerBuilder.Singleton.SetInstance(Infrastructure.AutofacContainerBuilder.Singleton.GetRootInstance());
            }
        }
    }
}
