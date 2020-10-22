using Microsoft.EntityFrameworkCore;

namespace RabbitsBurrow.Rabbit.Data.Context
{
    public class RabbitDbContext : DbContext
    {
        public RabbitDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Model.Rabbit> Rabbits { get; set; }
    }
}
