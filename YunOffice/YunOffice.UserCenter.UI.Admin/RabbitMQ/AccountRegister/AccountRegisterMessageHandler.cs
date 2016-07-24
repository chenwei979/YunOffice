using Autofac;
using System;
using YunOffice.Common.AOP;
using YunOffice.Common.RabbitMq;
using YunOffice.Common.Redis;
using YunOffice.UserCenter.BusnissLogic;
using YunOffice.UserCenter.Entities;
using YunOffice.UserCenter.UI.Admin.Models;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    public class AccountRegisterMessageHandler : MessageHandler<AccountRegisterViewModel>
    {
        public UserBusnissLogic BusnissLogic { get; set; }
        public RedisDataAccess<UserEntity> RedisDataAccess { get; set; }

        public AccountRegisterMessageHandler(IMqConfig config, RedisDataAccess<UserEntity> redisDataAccess) : base(config)
        {
            RedisDataAccess = redisDataAccess;
        }

        [AccountRegisterMessageHandActionExecutor]
        public override void Hand(AccountRegisterViewModel message)
        {
            //System.Threading.Thread.Sleep(5000);
            var entity = EmitMapperFactory.Singleton.GetInstance<AccountRegisterViewModel, UserEntity>(message);
            BusnissLogic.Save(entity);
            RedisDataAccess.Set(entity.Account, entity);
        }
    }



    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AccountRegisterMessageHandActionExecutorAttribute : Attribute, IActionExecutor
    {
        public dynamic Instance { get; set; }

        public ILifetimeScope DependencyResolver { get; set; }

        public virtual void OnActionExecuting()
        {
            if (!(Instance is AccountRegisterMessageHandler)) return;

            var instance = Instance as AccountRegisterMessageHandler;

            instance.BusnissLogic = DependencyResolver.Resolve<UserBusnissLogic>();

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
}
