using Entities.DomainEntities;
using Microsoft.EntityFrameworkCore;
using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;

namespace RelationalDatabase.Database;

public class AppDbContext : DbContext
{
    public DbSet<Deputado> Deputados { get; set; }
    public DbSet<DeputyExpenses> DeputadoExpenses { get; set; }
    public DbSet<DeputyWorkPresence> DeputadoWorkPresences { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}