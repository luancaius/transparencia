using Entities.DomainEntities;
using Microsoft.EntityFrameworkCore;
using RelationalDatabase.DTO;
using RelationalDatabase.DTO.Deputado;

namespace RelationalDatabase.Database;

public class AppDbContext : DbContext
{
    public DbSet<Deputado> Deputados { get; set; }
    public DbSet<Company> Empresas { get; set; }
    public DbSet<Supplier> Fornecedores { get; set; }
    public DbSet<DeputyExpense> DeputadoDespesas  { get; set; }
    public DbSet<Person> Pessoas  { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}