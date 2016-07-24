using Autofac;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace YunOffice.Common.DependencyInjection
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

        private AutofacContainerBuilder()
        {
            _builder = new ContainerBuilder();
            RegisterTypes();
            _container = _builder.Build();
        }

        public ILifetimeScope GetInstance()
        {
            return _container;
        }

        private void RegisterTypes()
        {
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            _builder.RegisterAssemblyModules(assemblies);
        }
    }
}
