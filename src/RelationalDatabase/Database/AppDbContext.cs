using Entities.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
}