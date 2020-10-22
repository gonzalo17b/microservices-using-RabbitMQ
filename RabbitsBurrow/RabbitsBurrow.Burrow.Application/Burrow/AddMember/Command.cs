using RabbitsBurrow.Domain.Commands;

namespace RabbitsBurrow.Burrow.Application.Burrow.AddMember
{
    public class AddMemberCommand : Command
    {
        public int Xcoordinate { get; private set; }
        public int Ycoordinate { get; private set; }

        public AddMemberCommand(int Xcoordinate, int Ycoordinate)
        {
            this.Xcoordinate = Xcoordinate;
            this.Ycoordinate = Ycoordinate;
        }
    }   
}
