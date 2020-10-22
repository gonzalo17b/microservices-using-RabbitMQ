using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitsBurrow.Infrastructure.Definitions
{
    public sealed class EventTypes
    {
        private List<Type> types = new List<Type>();
        public IReadOnlyCollection<Type> Types => types.AsReadOnly();

        private EventTypes() { }
        public static EventTypes New() => new EventTypes();

        public void Add(Type type)
        {
            if (!types.Contains(type))
                types.Add(type);
        }
        public Type GetTypeBy(string eventName) => types.SingleOrDefault(t => t.Name == eventName);
    }
}
