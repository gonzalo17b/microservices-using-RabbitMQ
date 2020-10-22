using RabbitsBurrow.Domain.Commands;
using RabbitsBurrow.Domain.Events;
using System.Threading.Tasks;

namespace RabbitsBurrow.Domain.Bus
{
    public interface IEventBus
    {
        Task SendCommand<TEvent>(TEvent command) where TEvent : Command;

        void Publish<TEvent>(TEvent @event) where TEvent : Event;

        void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;
    }
}
