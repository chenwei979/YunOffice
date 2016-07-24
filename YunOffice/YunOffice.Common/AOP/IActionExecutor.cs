using Autofac;

namespace YunOffice.Common.AOP
{
    public interface IActionExecutor
    {
        ILifetimeScope DependencyResolver { get; set; }
        dynamic Instance { get; set; }
        void OnActionExecuting();
        void OnActionExecuted();
    }
}
