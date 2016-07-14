using RabbitMQ.Client.Events;
using System;
using System.Text;
using YunOffice.UserCenter.Entities;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    public class AccountRegisterMessageHandler : MessageHandler<UserEntity>
    {
        public override void Hand(UserEntity message)
        {
            
        }
    }
}
