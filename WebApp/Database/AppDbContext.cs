using Microsoft.EntityFrameworkCore;
using WebApp.Configuration;
using WebApp.Models;

namespace WebApp.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
    }
}