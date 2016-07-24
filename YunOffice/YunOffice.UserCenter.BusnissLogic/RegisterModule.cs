using Autofac;

namespace YunOffice.UserCenter.BusnissLogic
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserBusnissLogic>();
        }
    }
}
