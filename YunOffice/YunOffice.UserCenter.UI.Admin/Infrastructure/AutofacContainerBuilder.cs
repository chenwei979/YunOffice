using Autofac;
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
        private IContainer _container;

        private AutofacContainerBuilder()
        {
            _builder = new ContainerBuilder();
            RegisterTypes();
            _container = _builder.Build();
        }

        public IContainer GetInstance()
        {
            return _container;
        }

        /// <summary>
        /// 容器注入BLL, DAL层
        /// </summary>
        private void RegisterTypes()
        {
            RegisterControllers();

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(item => item.FullName.StartsWith("Ak.", StringComparison.InvariantCultureIgnoreCase)).ToArray();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            _builder.RegisterTypes(types.Where(t => t.Name.EndsWith("BLL")).ToArray()).PropertiesAutowired();
            _builder.RegisterTypes(types.Where(t => t.Name.EndsWith("DAL")).ToArray()).PropertiesAutowired();

            //_builder.RegisterType(typeof(YunOffice.UserCenter.Entities.AkDbContext)).As(typeof(Ak.Entities.IDbContext)).InstancePerLifetimeScope();
            //_builder.RegisterType(typeof(Ak.Entities.EfUnitOfWork)).As(typeof(Ak.Entities.IUnitOfWork)).InstancePerLifetimeScope();
            //_builder.RegisterGeneric(typeof(Ak.Entities.EfRepository<>)).As(typeof(Ak.Entities.IRepository<>)).InstancePerLifetimeScope();
        }

        /// <summary>
        /// 容器注入Controllers
        /// </summary>
        private void RegisterControllers()
        {
            var mvcAssembly = Assembly.GetExecutingAssembly();
            _builder.RegisterAssemblyTypes(mvcAssembly).Where(t => t.Name.EndsWith("Controller")).AssignableTo(typeof(IController)).PropertiesAutowired();
        }
    }
}