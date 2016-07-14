using ProtoBuf;
using System.IO;
using YunOffice.UserCenter.Entities;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister
{
    public class AccountRegisterMessagePublisher : MessagePublisher<UserEntity>
    {
        public override void Push(UserEntity message)
        {
            var bytes = Serialize(message);
            Channel.BasicPublish(exchange: string.Empty, routingKey: "hello", basicProperties: null, body: bytes);
        }
    }
}
