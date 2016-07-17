using Castle.Core;
using Castle.DynamicProxy;
using System;
using System.Linq;
using YunOffice.UserCenter.BusnissLogic;
using YunOffice.UserCenter.Entities;
using YunOffice.UserCenter.UI.Admin.Models;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    [Interceptor(typeof(ActionExecutorInterceptor))]
    public class AccountRegisterMessageHandler : MessageHandler<AccountRegisterViewModel>
    {
        public UserBusnissLogic BusnissLogic { get; set; }

        public AccountRegisterMessageHandler(IMqConfig config, UserBusnissLogic busnissLogic) : base(config)
        {
            BusnissLogic = busnissLogic;
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
            Instance.BusnissLogic = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(UserBusnissLogic)) as UserBusnissLogic;
        }

        public virtual void OnActionExecuted()
        {
            if (Instance.BusnissLogic == null) return;

            Instance.BusnissLogic.Dispose();
        }
    }

    public class ActionExecutorInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var actionExecutors = invocation.Method.GetCustomAttributes(true).Where(item => item.GetType().GetGenericTypeDefinition() == typeof(IActionExecutor)).Select(item =>
            {
                return item as IActionExecutor;
            }).ToList();

            actionExecutors.ForEach(item =>
            {
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
