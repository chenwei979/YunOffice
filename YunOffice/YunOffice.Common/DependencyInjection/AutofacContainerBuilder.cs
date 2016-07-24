using Autofac;

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
        private ILifetimeScope _rootContainer;

        private AutofacContainerBuilder()
        {
            _builder = new ContainerBuilder();
            RegisterTypes();
            _rootContainer = _builder.Build();
            _container = _rootContainer;
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

        private void RegisterTypes()
        {
        }
    }
}
