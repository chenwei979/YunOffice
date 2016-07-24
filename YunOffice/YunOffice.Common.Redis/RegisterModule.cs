using Autofac;
using StackExchange.Redis;

namespace YunOffice.Common.Redis
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var redisManager = ConnectionMultiplexer.Connect("192.168.232.128");
            builder.RegisterInstance(redisManager).As<ConnectionMultiplexer>().SingleInstance();
            builder.RegisterGeneric(typeof(RedisDataAccess<>)).As(typeof(RedisDataAccess<>));
        }
    }
}
