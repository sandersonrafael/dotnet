using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AuthorModel> Authors { get; set; }
    public DbSet<BookModel> Books { get; set; }
}
