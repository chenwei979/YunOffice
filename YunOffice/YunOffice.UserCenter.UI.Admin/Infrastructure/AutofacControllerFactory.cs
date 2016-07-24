using Autofac;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using YunOffice.Common.DependencyInjection;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)AutofacContainerBuilder.Singleton.GetRootInstance().Resolve(controllerType);
        }
    }
}