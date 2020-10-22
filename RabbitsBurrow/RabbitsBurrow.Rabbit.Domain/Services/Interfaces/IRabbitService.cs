using System.Collections.Generic;

namespace RabbitsBurrow.Rabbit.Domain.Services.Interfaces
{
    public interface IRabbitService
    {
        IEnumerable<Model.Rabbit> All();
        void Add(Model.Rabbit rabbit);
    }
}
