using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.Domain.Commands;
using RabbitsBurrow.Domain.Events;
using RabbitsBurrow.Infrastructure.Definitions;
using RabbitsBurrow.Infrastructure.FixtureBuilder;
using System;
using System.Threading.Tasks;

namespace RabbitsBurrow.Infrastructure.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly HandlerTypes _HandlerTypes;

        private readonly IMediator mediator;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            Ensure.ThatIsNotNull(mediator);
            this.mediator = mediator;

            Ensure.ThatIsNotNull(serviceScopeFactory);
            this.serviceScopeFactory = serviceScopeFactory;

            _HandlerTypes = HandlerTypes.New();
        }

        public Task SendCommand<TEvent>(TEvent command) where TEvent : Command
        {
            return mediator.Send(command);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            RabbitFixtureBuilder.New("localhost", false)
                .WithOpenConection()
                .WithChannel()
                .QueueDeclare<TEvent>()
                .BasicPublish(@event);
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            _HandlerTypes.Add<TEvent, TEventHandler>();

            RabbitFixtureBuilder.New("localhost", true)
               .WithOpenConection()
               .WithChannel()
               .QueueDeclare<TEvent>()
               .BasicConsume<TEvent>(Consumer_Received);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    await RabbitProcessFixtureBuilder.New(_HandlerTypes, scope)
                       .ProcessEvent(e)
                       .ConfigureAwait(false);
                }                   
            }
            catch (Exception ex)
            {
                //TODO_GBC
            }
        }
    }
}
