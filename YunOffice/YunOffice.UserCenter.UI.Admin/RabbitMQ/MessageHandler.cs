using ProtoBuf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;

namespace YunOffice.UserCenter.UI.Admin.RabbitMQ
{
    public abstract class MessageHandler<TMessage> : IMessageHandler<TMessage>
    {
        protected IConnection Connection { get; set; }
        protected IModel Channel { get; set; }
        protected EventingBasicConsumer Consumer { get; set; }
        public string QueueName { get; set; }

        public MessageHandler(IMqConfig config)
        {
            var factory = new ConnectionFactory() { HostName = config.HostName, UserName = config.UserName, Password = config.Password };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();

            QueueName = "YunOffice.UserCenter.UI.Admin.RabbitMQ.AccountRegister.AccountRegisterMessage";//this.GetType().FullName.Replace("Handler", string.Empty);
            Channel.QueueDeclare(queue: QueueName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            Consumer = new EventingBasicConsumer(Channel);
            Consumer.Received += (model, ea) =>
            {
                var message = Deserialize(ea.Body);
                Hand(message);
            };
            Channel.BasicConsume(queue: QueueName, noAck: true, consumer: Consumer);
        }

        public void Dispose()
        {
            Channel.Dispose();
            Connection.Dispose();
        }

        public abstract void Hand(TMessage message);

        public TMessage Deserialize(byte[] message)
        {
            //string json = System.Text.Encoding.UTF8.GetString(message);
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<TMessage>(json);

            var stream = new MemoryStream(message, false);
            stream.Position = 0;
            return Serializer.Deserialize<TMessage>(stream);
        }
    }
}
