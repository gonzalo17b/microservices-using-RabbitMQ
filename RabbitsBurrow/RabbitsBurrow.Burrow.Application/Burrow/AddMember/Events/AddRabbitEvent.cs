using RabbitsBurrow.Domain.Events;

namespace RabbitsBurrow.Burrow.Application.Burrow.AddMember.Events
{
    public class AddRabbitEvent : Event
    {
        public int Xcoordinate { get; private set; }
        public int Ycoordinate { get; private set; }
        public int BurrowId { get; private set; }
        
        public AddRabbitEvent(int burrowId, int Xcoordinate, int Ycoordinate)
        {
            this.Xcoordinate = Xcoordinate;
            this.Ycoordinate = Ycoordinate;
            this.BurrowId = burrowId;
        }
    }
}
