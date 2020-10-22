using RabbitsBurrow.Infrastructure;
using RabbitsBurrow.Rabbit.Data.Context;
using RabbitsBurrow.Rabbit.Domain.IRepository;
using System.Collections.Generic;

namespace RabbitsBurrow.Rabbit.Data.Repositories
{
    public class RabbitRepository : IRabbitRepository
    {
        private readonly RabbitDbContext ctx;
        public RabbitRepository(RabbitDbContext ctx)
        {
            Ensure.ThatIsNotNull(ctx);
            this.ctx = ctx;
        }

        public IEnumerable<Domain.Model.Rabbit> All() => ctx.Rabbits;
        public void Add(Domain.Model.Rabbit rabbit)
        {
            ctx.Rabbits.Add(rabbit);
            ctx.SaveChanges();
        }
    }
}
