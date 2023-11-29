using Entities.DomainEntities;
using Microsoft.EntityFrameworkCore;
using RelationalDatabase.DTO;

namespace RelationalDatabase.Database;

public class AppDbContext : DbContext
{
    public DbSet<Deputado> Deputados { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}