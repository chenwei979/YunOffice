using Autofac;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using YunOffice.Common.DependencyInjection;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        protected ILifetimeScope Container
        {
            get
            {
                return AutofacContainerBuilder.Singleton.GetInstance();
            }
        }

        public object GetService(Type serviceType)
        {
            if (Container.IsRegistered(serviceType)) return Container.Resolve(serviceType);
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(new Type[]
            {
                serviceType
            });
            var obj = Container.Resolve(type);
            return obj as IEnumerable<object>;
        }
    }
}