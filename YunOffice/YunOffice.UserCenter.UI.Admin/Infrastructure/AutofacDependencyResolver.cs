/*
 *auhor: Bruce
 *date: 20160413
 *function:1.引入autoFac
 *         2.实现IDependencyResolver已实现依赖注入
 * 
 */
using Autofac;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private IContainer _container;

        public AutofacDependencyResolver()
        {
            _container = AutofacContainerBuilder.Singleton.GetInstance();
        }

        public object GetService(Type serviceType)
        {
            if (_container.IsRegistered(serviceType)) return _container.Resolve(serviceType);
            return null;

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				serviceType
			});
            var obj = _container.Resolve(type);
            return obj as IEnumerable<object>;
        }
    }
}