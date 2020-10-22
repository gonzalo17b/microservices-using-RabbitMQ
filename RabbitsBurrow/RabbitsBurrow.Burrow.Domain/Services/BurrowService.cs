using RabbitsBurrow.Burrow.Domain.IRepository;
using RabbitsBurrow.Burrow.Domain.Services.Interfaces;
using RabbitsBurrow.Infrastructure;
using System.Collections.Generic;

namespace RabbitsBurrow.Burrow.Domain.Services
{
    public class BurrowService : IBurrowService
    {
        private readonly IBurrowRepository burrowRepository;

        public BurrowService(IBurrowRepository burrowRepository)
        {
            Ensure.ThatIsNotNull(burrowRepository);
            this.burrowRepository = burrowRepository;
        }

        public IEnumerable<Model.Burrow> All() => this.burrowRepository.All();
        public Model.Burrow First() => this.burrowRepository.First();  
        public void AddMember(int burrowId)
        {
            var burrow = this.burrowRepository.Find(burrowId);
            burrow.AddMember();
            this.burrowRepository.Update(burrow);
        }
    }
}
