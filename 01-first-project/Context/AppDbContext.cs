using _01_first_project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01_first_project.Context;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /* Changing Properties */

    // It is possible to change the columns definition in SQL using that override method: OnModelCreating
    // But it's possible too using the annotations in Entity class -> Recommended

    // This is what we call: Fluent Api
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(500).IsRequired(false);
        modelBuilder.Entity<Product>()
            .Property(p => p.Name).HasMaxLength(120).IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Code).HasMaxLength(20).IsRequired();
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer
    //    (
    //        "Server=localhost,1433;Database=Products;User Id=sa;Password=Senha@123;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES"
    //    );
    //}
}
