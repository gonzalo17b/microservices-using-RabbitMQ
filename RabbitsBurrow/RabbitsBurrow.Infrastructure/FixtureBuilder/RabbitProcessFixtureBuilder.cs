using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.Infrastructure.Definitions;
using System.Text;
using System.Threading.Tasks;

namespace RabbitsBurrow.Infrastructure.FixtureBuilder
{
    public sealed class RabbitProcessFixtureBuilder
    {
        private readonly HandlerTypes handlerTypes;
        private readonly IServiceScope scope;

        private RabbitProcessFixtureBuilder(HandlerTypes handlerTypes, IServiceScope scope)
        {
            Ensure.ThatIsNotNull(handlerTypes);
            this.handlerTypes = handlerTypes;
            
            Ensure.ThatIsNotNull(scope);
            this.scope = scope;
        }

        public async Task ProcessEvent(BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            if (handlerTypes.IsASubscribedEvent(eventName))
            {
                var subscriptions = handlerTypes.GetHandlerBy(eventName);
                foreach (var subscription in subscriptions.Types)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);
                    if (handler == null) 
                        continue;

                    var eventType = handlerTypes.EventTypes.GetTypeBy(eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }

        public static RabbitProcessFixtureBuilder New(HandlerTypes handlerTypes, IServiceScope scope) 
            => new RabbitProcessFixtureBuilder(handlerTypes, scope);
    }
}
