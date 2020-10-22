using System.Collections.Generic;

namespace RabbitsBurrow.Burrow.Domain.Services.Interfaces
{
    public interface IBurrowService
    {
        IEnumerable<Model.Burrow> All();
        Model.Burrow First();

        void AddMember(int burrowId);
    }
}
