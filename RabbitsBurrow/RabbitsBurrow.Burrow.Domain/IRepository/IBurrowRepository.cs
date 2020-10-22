using System.Collections.Generic;

namespace RabbitsBurrow.Burrow.Domain.IRepository
{
    public interface IBurrowRepository
    {
        Model.Burrow First();
        Model.Burrow Find(int id);
        IEnumerable<Model.Burrow> All();

        void Update(Model.Burrow borrow);
    }
}
