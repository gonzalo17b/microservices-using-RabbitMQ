using RabbitsBurrow.Burrow.Data.Context;
using RabbitsBurrow.Burrow.Domain.IRepository;
using RabbitsBurrow.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace RabbitsBurrow.Burrow.Data.Repositories
{
    public class BurrowRepository : IBurrowRepository
    {
        private readonly BurrowDbContext ctx;
        public BurrowRepository(BurrowDbContext ctx)
        {
            Ensure.ThatIsNotNull(ctx);
            this.ctx = ctx;
        }

        public IEnumerable<Domain.Model.Burrow> All() => ctx.Burrows;
        public Domain.Model.Burrow First() => ctx.Burrows.First();
        public Domain.Model.Burrow Find(int id) => ctx.Burrows.Single(x => x.Id == id);
        public void Update(Domain.Model.Burrow borrow)
        {
            ctx.Burrows.Update(borrow);
            ctx.SaveChanges();
        }
    }
}
