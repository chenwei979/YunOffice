using ProtoBuf;
using RabbitMQ.Client;
using System.IO;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ
{
    public abstract class MessagePublisher<TMessage> : IMessagePublisher<TMessage>
    {
        protected IConnection Connection { get; set; }
        protected IModel Channel { get; set; }
        public string QueueName { get; set; }

        public MessagePublisher(IMqConfig config)
        {
            var factory = new ConnectionFactory() { HostName = config.HostName, UserName = config.UserName, Password = config.Password };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
            QueueName = this.GetType().FullName.Replace("Publisher", string.Empty);
            Channel.QueueDeclare(queue: QueueName,
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
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            //return System.Text.Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, message);
                return stream.ToArray();
            }
        }
    }
}
