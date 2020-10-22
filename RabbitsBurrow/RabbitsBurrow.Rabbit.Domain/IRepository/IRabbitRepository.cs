using System.Collections.Generic;

namespace RabbitsBurrow.Rabbit.Domain.IRepository
{
    public interface IRabbitRepository
    {
        IEnumerable<Model.Rabbit> All();
        void Add(Model.Rabbit rabit);
    }
}
