using Microsoft.EntityFrameworkCore;

namespace RabbitsBurrow.Burrow.Data.Context
{
    public class BurrowDbContext : DbContext
    {
        public BurrowDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Model.Burrow> Burrows { get; set; }
    }
}
