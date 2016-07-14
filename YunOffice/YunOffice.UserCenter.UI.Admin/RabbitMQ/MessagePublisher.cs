using System;
using RabbitMQ.Client;
using System.IO;
using ProtoBuf;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ
{
    public abstract class MessagePublisher<TMessage> : IMessagePublisher<TMessage>
    {
        protected IConnection Connection { get; set; }
        protected IModel Channel { get; set; }

        public MessagePublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(queue: this.GetType().FullName.Replace("Publisher", string.Empty),
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void Dispose()
        {
            Channel.Dispose();
            Connection.Dispose();
        }

        public abstract void Push(TMessage message);

        public byte[] Serialize(TMessage message)
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, message);
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}
