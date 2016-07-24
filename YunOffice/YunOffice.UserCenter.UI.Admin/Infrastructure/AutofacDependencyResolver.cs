using Autofac;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using YunOffice.Common.DependencyInjection;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        protected ILifetimeScope Container { get; set; }

        public AutofacDependencyResolver()
        {
            Container = AutofacContainerBuilder.Singleton.GetInstance().BeginLifetimeScope("AutofacWebRequest");
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