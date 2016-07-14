using System;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ
{
    public interface IMessageHandler<TMessage> : IDisposable
    {
        void Hand(TMessage message);

        TMessage Deserialize(byte[] message);
    }
}
