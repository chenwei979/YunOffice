using System;

namespace YunOffice.Common.RabbitMq
{
    public interface IMessageHandler<TMessage> : IDisposable
    {
        void Hand(TMessage message);

        TMessage Deserialize(byte[] message);
    }
}
