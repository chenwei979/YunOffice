using Autofac;
using Autofac.Extras.DynamicProxy2;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using YunOffice.Common.AOP;
using YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister;

namespace YunOffice.UserCenter.UI.Admin
{
    public class RegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AccountRegisterMessagePublisher)).As(typeof(AccountRegisterMessagePublisher)).SingleInstance();
            builder.RegisterType<AccountRegisterMessageHandler>().EnableClassInterceptors().InterceptedBy(typeof(ActionExecutorInterceptor)).AutoActivate();

            var mvcAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(mvcAssembly).Where(t => t.Name.EndsWith("Controller")).AssignableTo(typeof(IController));
        }
    }
}