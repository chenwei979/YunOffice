using Autofac;
using System;
using System.Web.Mvc;
using System.Web.Routing;


namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacControllerFactory : DefaultControllerFactory
    {
        private IContainer _container;

        public AutofacControllerFactory()
        {
            _container = AutofacContainerBuilder.Singleton.GetInstance();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_container.Resolve(controllerType);
        }
    }
}