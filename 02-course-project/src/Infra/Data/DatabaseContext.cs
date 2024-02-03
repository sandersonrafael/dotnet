using FinalProject.Domain.Products;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infra.Data;

public class DatabaseContext : IdentityDbContext<IdentityUser> // To use Identity package, change DbContext to IdentityDbContext<IdentityUser>
{ // Add service no Program.cs builder's // Add changes to OnModelCreating method down
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder model)
    {
        // Calling father class method OnModelCreating and attrib his properties on
            // that children class characteristics inside OnModelCreating
        base.OnModelCreating(model);

        // Ignoring attribute Notification inside the entities
        model.Ignore<Notification>();

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
