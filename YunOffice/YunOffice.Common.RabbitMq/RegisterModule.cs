using Autofac;

namespace YunOffice.Common.RabbitMq
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MqConfig)).As(typeof(IMqConfig)).SingleInstance();
        }
    }
}
