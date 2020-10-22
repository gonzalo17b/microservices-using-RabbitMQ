using RabbitsBurrow.Infrastructure;
using RabbitsBurrow.Rabbit.Domain.IRepository;
using RabbitsBurrow.Rabbit.Domain.Services.Interfaces;
using System.Collections.Generic;

namespace RabbitsBurrow.Rabbit.Domain.Services
{
    public class RabbitService : IRabbitService
    {
        private readonly IRabbitRepository rabbitRepository;

        public RabbitService(IRabbitRepository rabbitRepository)
        {
            Ensure.ThatIsNotNull(rabbitRepository);
            this.rabbitRepository = rabbitRepository;
        }

        public IEnumerable<Model.Rabbit> All() => this.rabbitRepository.All();
        public void Add(Model.Rabbit rabbit) => this.rabbitRepository.Add(rabbit);
    }
}
