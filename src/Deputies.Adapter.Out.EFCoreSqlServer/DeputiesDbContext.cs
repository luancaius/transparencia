using Deputies.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deputies.Adapter.Out.EFCoreSqlServer
{
    public class DeputiesDbContext : DbContext
    {
        public DeputiesDbContext(DbContextOptions<DeputiesDbContext> options)
            : base(options)
        {
        }
        public DbSet<Deputy> Deputies { get; set; }
    }
}