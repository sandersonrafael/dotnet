using FinalProject.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infra.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        model.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);

        model.Entity<Category>()
            .Property(p => p.Name).IsRequired();
    }

    // This class that extends DbContext have some configurations to Database connection.
    // Like conventions(how the application treats specific datas by default, like: Id Attribute IS PRIMARY KEY):
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        // By default, the string attributes will have max length of 100 characters
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}
