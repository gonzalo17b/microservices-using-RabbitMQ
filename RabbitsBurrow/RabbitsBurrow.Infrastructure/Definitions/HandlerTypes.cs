using RabbitsBurrow.Domain.Bus;
using RabbitsBurrow.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitsBurrow.Infrastructure.Definitions
{
    public sealed class HandlerTypes
    {
        public Dictionary<string, EventTypes> Handlers { get; private set; }
        public EventTypes EventTypes { get; private set; }

        private HandlerTypes() 
        {
            Handlers = new Dictionary<string, EventTypes>();
            EventTypes = EventTypes.New();
        }
        public static HandlerTypes New() => new HandlerTypes();

        public void Add<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);
            EventTypes.Add(eventType);

            var handlerType = typeof(TEventHandler);
            var eventName = eventType.Name;
            if (Handlers.ContainsKey(eventName))
            {
                var handler = Handlers[eventName];

                var isRegistered = handler.Types.Any(s => s.GetType() == handlerType);
                Ensure.That<ArgumentException>(isRegistered, $"Handler Type {handlerType.Name} already is registered for '{eventName}'");
            }

            var newType = EventTypes.New();
            newType.Add(handlerType);
            Handlers.Add(eventName, newType);
        }

        public EventTypes GetHandlerBy(string eventName) => Handlers[eventName];
        public bool IsASubscribedEvent(string eventName) => Handlers.ContainsKey(eventName);
    }
}