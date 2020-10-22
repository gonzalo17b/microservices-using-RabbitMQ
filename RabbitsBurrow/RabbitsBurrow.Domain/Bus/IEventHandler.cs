using RabbitsBurrow.Domain.Events;
using System.Threading.Tasks;

namespace RabbitsBurrow.Domain.Bus
{
    public interface IEventHandler<in TEvent> where TEvent : Event
    {
        Task Handle(TEvent @event);
    }
}
