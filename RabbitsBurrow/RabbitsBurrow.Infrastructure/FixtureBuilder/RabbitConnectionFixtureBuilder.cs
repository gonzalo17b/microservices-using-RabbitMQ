using RabbitMQ.Client;

namespace RabbitsBurrow.Infrastructure.FixtureBuilder
{
    public sealed class RabbitConnectionFixtureBuilder
    {
        private readonly IConnection connection;
        private readonly RabbitFixtureBuilder rabbitFactory;
        private RabbitChannelFixtureBuilder rabbitChannel;

        private RabbitConnectionFixtureBuilder(RabbitFixtureBuilder rabbitFactory, IConnection connection)
        {
            Ensure.ThatIsNotNull(rabbitFactory);
            this.rabbitFactory = rabbitFactory;

            Ensure.ThatIsNotNull(connection);
            this.connection = connection;
        }

        public RabbitFixtureBuilder Factory() => rabbitFactory;
        public RabbitChannelFixtureBuilder Channel() => rabbitChannel;

        public RabbitChannelFixtureBuilder WithChannel()
        {
            var channel = this.connection.CreateModel();
            this.rabbitChannel = RabbitChannelFixtureBuilder.New(this, channel);
            return this.rabbitChannel;
        }

        public RabbitConnectionFixtureBuilder Dispose()
        {
            this.connection.Dispose();
            return this;
        }
        public static RabbitConnectionFixtureBuilder New(RabbitFixtureBuilder factory, IConnection connection) 
            => new RabbitConnectionFixtureBuilder(factory, connection);
    }
}
