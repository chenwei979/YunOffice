using System;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ
{
    public interface IMessagePublisher<TMessage> : IDisposable
    {
        void Push(TMessage message);

        byte[] Serialize(TMessage message);
    }
}
