﻿using Autofac;
using YunOffice.Common.AOP;
using YunOffice.Common.DataAccess;

namespace YunOffice.Common
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(SqlServerDatabase)).As(typeof(IDatabase)).InstancePerMatchingLifetimeScope("AutofacWebRequest", "ActionExecutorInterceptor");
            //builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerMatchingLifetimeScope("AutofacWebRequest", "ActionExecutorInterceptor");
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.Register(c => new ActionExecutorInterceptor());
        }
    }
}
