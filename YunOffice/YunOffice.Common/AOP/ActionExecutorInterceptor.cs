using Castle.DynamicProxy;
using System;
using System.Linq;
using YunOffice.Common.DependencyInjection;

namespace YunOffice.Common.AOP
{
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

            using (var scope = AutofacContainerBuilder.Singleton.GetRootInstance().BeginLifetimeScope("ActionExecutorInterceptor"))
            {
                actionExecutors.ForEach(item =>
                {
                    item.Instance = invocation.InvocationTarget;
                    item.DependencyResolver = scope;
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
