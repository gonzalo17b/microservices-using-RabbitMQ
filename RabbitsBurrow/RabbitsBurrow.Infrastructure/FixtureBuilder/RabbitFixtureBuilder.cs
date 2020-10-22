using RabbitMQ.Client;

namespace RabbitsBurrow.Infrastructure.FixtureBuilder
{
    public sealed class RabbitFixtureBuilder
    {
        private readonly ConnectionFactory factory;
        private RabbitConnectionFixtureBuilder rabbitConnection;

        private RabbitFixtureBuilder(string hostName, bool dispatchConsumersAsync)
        {
            Ensure.ThatIsNotNull(hostName);
            this.factory = new ConnectionFactory() { HostName = hostName, DispatchConsumersAsync = dispatchConsumersAsync };
        }

        public RabbitConnectionFixtureBuilder Connection() => rabbitConnection;

        public RabbitConnectionFixtureBuilder WithOpenConection()
        {
            var connection = this.factory.CreateConnection();
            this.rabbitConnection = RabbitConnectionFixtureBuilder.New(this, connection);
            return this.rabbitConnection;
        }

        public static RabbitFixtureBuilder New(string hostName, bool dispatchConsumersAsync) => new RabbitFixtureBuilder(hostName, dispatchConsumersAsync);
    }
}
