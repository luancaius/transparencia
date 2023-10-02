using Entities.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.Database;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Deputy> Deputies { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}