using Autofac;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using YunOffice.Common.DependencyInjection;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacControllerFactory : DefaultControllerFactory
    {
        protected ILifetimeScope Container { get; set; }

        public AutofacControllerFactory()
        {
            Container = AutofacContainerBuilder.Singleton.GetRootInstance().BeginLifetimeScope("AutofacWebRequest");
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)Container.Resolve(controllerType);
        }
    }
}