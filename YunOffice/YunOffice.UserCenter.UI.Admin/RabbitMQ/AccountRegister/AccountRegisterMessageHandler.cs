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
        dynamic Instance { get; set; }
        void OnActionExecuting();
        void OnActionExecuted();

    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AccountRegisterMessageHandActionExecutorAttribute : Attribute, IActionExecutor
    {
        public dynamic Instance { get; set; }

        public virtual void OnActionExecuting()
        {
            if (!(Instance is AccountRegisterMessageHandler)) return;

            var instance = Instance as AccountRegisterMessageHandler;
            instance.BusnissLogic = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(UserBusnissLogic)) as UserBusnissLogic;
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

            actionExecutors.ForEach(item =>
            {
                item.Instance = invocation.InvocationTarget;
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
