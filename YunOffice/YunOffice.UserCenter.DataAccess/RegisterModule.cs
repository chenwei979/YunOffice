using Autofac;

namespace YunOffice.UserCenter.DataAccess
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserDataAccess>();
        }
    }
}
