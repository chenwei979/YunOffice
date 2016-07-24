using Autofac;
using Autofac.Extras.DynamicProxy2;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class AutofacContainerBuilder
    {
        public static AutofacContainerBuilder Singleton { get; private set; }

        static AutofacContainerBuilder()
        {
            Singleton = new AutofacContainerBuilder();
        }

        private ContainerBuilder _builder;
        private ILifetimeScope _container;
        private ILifetimeScope _rootContainer;

        private AutofacContainerBuilder()
        {
            _builder = new ContainerBuilder();
            RegisterTypes();
            _rootContainer = _builder.Build();
            _container = _rootContainer;
            _container.Resolve<RabbitMQ.AccountRegister.AccountRegisterMessageHandler>();
        }

        public ILifetimeScope GetRootInstance()
        {
            return _rootContainer;
        }

        public ILifetimeScope GetInstance()
        {
            return _container;
        }

        public void SetInstance(ILifetimeScope container)
        {
            _container = container;
        }

        /// <summary>
        /// 容器注入BLL, DAL层
        /// </summary>
        private void RegisterTypes()
        {
            RegisterControllers();

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(item => item.FullName.StartsWith("YunOffice.UserCenter", StringComparison.InvariantCultureIgnoreCase)).ToArray();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());


            _builder.RegisterType(typeof(RabbitMQ.AccountRegister.AccountRegisterMessagePublisher)).As(typeof(RabbitMQ.AccountRegister.AccountRegisterMessagePublisher)).SingleInstance();
            _builder.RegisterType<RabbitMQ.AccountRegister.AccountRegisterMessageHandler>().EnableClassInterceptors().InterceptedBy(typeof(Common.AOP.ActionExecutorInterceptor));
            _builder.Register(c => new Common.AOP.ActionExecutorInterceptor());
        }

        /// <summary>
        /// 容器注入Controllers
        /// </summary>
        private void RegisterControllers()
        {
            var mvcAssembly = Assembly.GetExecutingAssembly();
            _builder.RegisterAssemblyTypes(mvcAssembly).Where(t => t.Name.EndsWith("Controller")).AssignableTo(typeof(IController));
        }
    }
}