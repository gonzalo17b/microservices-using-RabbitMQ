using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitsBurrow.Domain.Events;
using System.Text;

namespace RabbitsBurrow.Infrastructure.FixtureBuilder
{
    public sealed class RabbitChannelFixtureBuilder
    {
        private readonly IModel channel;
        private readonly RabbitConnectionFixtureBuilder connection;

        private RabbitChannelFixtureBuilder(RabbitConnectionFixtureBuilder rabbitConnection, IModel channel)
        {
            Ensure.ThatIsNotNull(rabbitConnection);
            this.connection = rabbitConnection;

            Ensure.ThatIsNotNull(channel);
            this.channel = channel;
        }

        public RabbitConnectionFixtureBuilder Connection() => this.connection;
        public RabbitFixtureBuilder Factory() => this.connection.Factory();

        public RabbitChannelFixtureBuilder QueueDeclare<TEvent>() where TEvent : Event
        {
            var eventName = typeof(TEvent).Name;
            this.channel.QueueDeclare(eventName, false, false, false, null);
            return this;
        }

        public RabbitChannelFixtureBuilder BasicPublish<TEvent>(TEvent @event, bool dispose = true) where TEvent : Event
        {
            var eventName = typeof(TEvent).Name;
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            this.channel.BasicPublish("", eventName, null, body);
            if (dispose)
            {
                this.channel.Dispose();
                this.connection.Dispose();
            }
            return this;
        }

        public RabbitChannelFixtureBuilder BasicConsume<TEvent>(AsyncEventHandler<BasicDeliverEventArgs> received) where TEvent : Event
        {
            var eventName = typeof(TEvent).Name;
            var consumer = new AsyncEventingBasicConsumer(this.channel);
            consumer.Received += received;
            this.channel.BasicConsume(eventName, true, consumer);
            return this;
        }

        public static RabbitChannelFixtureBuilder New(RabbitConnectionFixtureBuilder rabbitConnection,IModel channel)
            => new RabbitChannelFixtureBuilder(rabbitConnection, channel);
    }
}
