using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitsBurrow.Rabbit.Domain.Model
{
    public class Rabbit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }

        private Rabbit() { }

        public void SetXcoordinate(int xCoordinate) => this.XCoordinate = xCoordinate;
        public void SetYcoordinate(int yCoordinate) => this.YCoordinate = yCoordinate;

        public static Rabbit New() => new Rabbit();
    }
}
