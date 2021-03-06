﻿using YunOffice.Common.RabbitMq;
using YunOffice.UserCenter.UI.Admin.Models;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    public class AccountRegisterMessagePublisher : MessagePublisher<AccountRegisterViewModel>
    {
        public AccountRegisterMessagePublisher(IMqConfig config) : base(config)
        {
        }

        public override void Push(AccountRegisterViewModel message)
        {
            var bytes = Serialize(message);
            Channel.BasicPublish(exchange: string.Empty, routingKey: QueueName, basicProperties: null, body: bytes);
        }
    }
}
