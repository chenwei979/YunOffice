using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;
using YunOffice.Common.DependencyInjection;

[assembly: OwinStartup(typeof(YunOffice.UserCenter.UI.Admin.App_Start.OwinStartup))]

namespace YunOffice.UserCenter.UI.Admin.App_Start
{
    public partial class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(typeof(AutofacWebRequestLifetimeScopeMiddleware));

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
            using (var scope = AutofacContainerBuilder.Singleton.GetRootInstance().BeginLifetimeScope("AutofacWebRequest"))
            {
                await Next.Invoke(context);
            }
        }
    }
}
