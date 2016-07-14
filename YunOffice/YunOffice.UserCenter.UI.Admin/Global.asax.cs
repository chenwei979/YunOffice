using System.Web.Mvc;
using System.Web.Routing;
using YunOffice.UserCenter.UI.Admin.Infrastructure;

namespace YunOffice.UserCenter.UI.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //使用自定factory
            //ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory());
            //使用DependencyResolver实现依赖注入
            DependencyResolver.SetResolver(new AutofacDependencyResolver());
        }
    }
}
