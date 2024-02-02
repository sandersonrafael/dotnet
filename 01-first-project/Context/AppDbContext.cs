using _01_first_project.Models;
using Microsoft.EntityFrameworkCore;

namespace _01_first_project.Context;

public class AppDbContext : DbContext
{
    // To associate the DbSet to Database, it is necessary to create the tables. Let's add migrations for this1
    // To add migrations, we'll use the command: dotnet ef migrations add MigrationName
    // At last, to update the database with migrations, use the command: dotnet ef database update
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer
        (
        "Server=localhost,1433;Database=Products;User Id=sa;Password=senha123;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES"
        );
    }
}
