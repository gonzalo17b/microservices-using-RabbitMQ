using RabbitsBurrow.Domain.Events;
using System;

namespace RabbitsBurrow.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }
        protected Command() => Timestamp = DateTime.Now;
    }
}
