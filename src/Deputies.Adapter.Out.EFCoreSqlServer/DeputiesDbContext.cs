// Deputies.Adapter.Out.EFCoreSqlServer/DeputiesDbContext.cs

using Deputies.Adapter.Out.EFCoreSqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace Deputies.Adapter.Out.EFCoreSqlServer;

public class DeputiesDbContext : DbContext
{
    public DeputiesDbContext()
    {
    }
    public DeputiesDbContext(DbContextOptions<DeputiesDbContext> options)
        : base(options)
    {
    }

    public DbSet<PersonEfModel> Persons { get; set; }
    public DbSet<DeputyEfModel> Deputies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure a unique index on Cpf
        modelBuilder.Entity<PersonEfModel>()
            .HasIndex(p => p.Cpf)
            .IsUnique();
    }
}