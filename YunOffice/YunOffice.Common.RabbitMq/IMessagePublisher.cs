using System;

namespace YunOffice.Common.RabbitMq
{
    public interface IMessagePublisher<TMessage> : IDisposable
    {
        void Push(TMessage message);

        byte[] Serialize(TMessage message);
    }
}
