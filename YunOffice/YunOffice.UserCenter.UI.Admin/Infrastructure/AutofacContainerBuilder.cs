﻿using Autofac;
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
        private IContainer _container;

        private AutofacContainerBuilder()
        {
            _builder = new ContainerBuilder();
            RegisterTypes();
            _container = _builder.Build();
            _container.Resolve<RabbitMQ.AccountRegister.AccountRegisterMessageHandler>();
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

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(item => item.FullName.StartsWith("YunOffice.UserCenter", StringComparison.InvariantCultureIgnoreCase)).ToArray();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            _builder.RegisterTypes(types.Where(t => t.Name.EndsWith("BusnissLogic")).ToArray()).InstancePerLifetimeScope();
            _builder.RegisterTypes(types.Where(t => t.Name.EndsWith("DataAccess")).ToArray()).InstancePerLifetimeScope();
            _builder.RegisterType(typeof(Common.DataAccess.SqlServerDatabase)).As(typeof(Common.DataAccess.IDatabase)).InstancePerLifetimeScope();
            _builder.RegisterType(typeof(Common.DataAccess.UnitOfWork)).As(typeof(Common.DataAccess.IUnitOfWork)).InstancePerLifetimeScope();
            _builder.RegisterGeneric(typeof(Common.DataAccess.Repository<>)).As(typeof(Common.DataAccess.IRepository<>)).InstancePerLifetimeScope();

            _builder.RegisterType(typeof(RabbitMQ.MqConfig)).As(typeof(RabbitMQ.IMqConfig)).SingleInstance();
            _builder.RegisterType(typeof(RabbitMQ.AccountRegister.AccountRegisterMessagePublisher)).As(typeof(RabbitMQ.AccountRegister.AccountRegisterMessagePublisher)).SingleInstance();
            _builder.RegisterType<RabbitMQ.AccountRegister.AccountRegisterMessageHandler>().EnableClassInterceptors().InterceptedBy(typeof(RabbitMQ.AccountRegister.ActionExecutorInterceptor));
            _builder.Register(c => new RabbitMQ.AccountRegister.ActionExecutorInterceptor());
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