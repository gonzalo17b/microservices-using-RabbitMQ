namespace RabbitsBurrow.Burrow.Domain.Model
{
    public class Burrow
    {
        public int Id { get; private set; }
        public int MaximumX { get; private set; }
        public int MaximumY { get; private set; }
        public int MembersCounter { get; private set; }

        private Burrow() { }

        public void SetId(int id) => this.Id = id;
        public void SetMaximumX(int maximumX) => this.MaximumX = maximumX;
        public void SetMaximumY(int maximumY) => this.MaximumY = maximumY;
        public void AddMember() => this.MembersCounter++;

        public static Burrow New() => new Burrow();
    }
}
