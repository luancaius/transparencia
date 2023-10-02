using Microsoft.EntityFrameworkCore;

namespace Repository;

public class MyDbContext : DbContext
{
    // Other DbSet properties for your entities

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your database connection here
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}