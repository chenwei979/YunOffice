using Autofac;
using Castle.Core;
using Castle.DynamicProxy;
using System;
using System.Linq;
using YunOffice.UserCenter.BusnissLogic;
using YunOffice.UserCenter.Entities;
using YunOffice.UserCenter.UI.Admin.Models;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    public class AccountRegisterMessageHandler : MessageHandler<AccountRegisterViewModel>
    {
        public UserBusnissLogic BusnissLogic { get; set; }

        public AccountRegisterMessageHandler(IMqConfig config) : base(config)
        {
        }

        [AccountRegisterMessageHandActionExecutor]
        public override void Hand(AccountRegisterViewModel message)
        {
            var entity = EmitMapperFactory.Singleton.GetInstance<AccountRegisterViewModel, UserEntity>(message);
            BusnissLogic.Save(entity);
        }
    }

    public interface IActionExecutor
    {
        ILifetimeScope DIContainer { get; set; }
        dynamic Instance { get; set; }
        void OnActionExecuting();
        void OnActionExecuted();

    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AccountRegisterMessageHandActionExecutorAttribute : Attribute, IActionExecutor
    {
        public dynamic Instance { get; set; }

        public ILifetimeScope DIContainer { get; set; }

        public virtual void OnActionExecuting()
        {
            if (!(Instance is AccountRegisterMessageHandler)) return;

            var instance = Instance as AccountRegisterMessageHandler;

            instance.BusnissLogic = DIContainer.Resolve<UserBusnissLogic>();

        }

        public virtual void OnActionExecuted()
        {
            if (!(Instance is AccountRegisterMessageHandler)) return;

            var instance = Instance as AccountRegisterMessageHandler;
            if (instance.BusnissLogic == null) return;

            instance.BusnissLogic.Dispose();
            instance.BusnissLogic = null;
        }
    }

    public class ActionExecutorInterceptor : IInterceptor
    {
        public ActionExecutorInterceptor()
        {
            Console.Write("ActionExecutorInterceptor");
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            var actionExecutors = invocation.Method.GetCustomAttributes(true).Where(item => item is IActionExecutor).Select(item =>
            {
                return item as IActionExecutor;
            }).ToList();

            using (var scope = Infrastructure.AutofacContainerBuilder.Singleton.GetRootInstance().BeginLifetimeScope("MqHandler"))
            {
                actionExecutors.ForEach(item =>
                {
                    item.Instance = invocation.InvocationTarget;
                    item.DIContainer = scope;
                    item.OnActionExecuting();
                });

                invocation.Proceed();

                actionExecutors.ForEach(item =>
                {
                    item.OnActionExecuted();
                });
            }
        }
    }
}
