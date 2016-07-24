using Autofac;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunOffice.Common.Redis
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var redisManager = ConnectionMultiplexer.Connect("localhost");
            builder.RegisterInstance(redisManager).As<ConnectionMultiplexer>().SingleInstance();
        }
    }
}
