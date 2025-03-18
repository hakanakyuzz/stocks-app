using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Configuration;
using WebApp.Models;

namespace WebApp.Database;

// AppDbContext : Represents the connection to the database
// DbContext : EF Core, here are my models. Create tables for them
// IdentityDbContext : Also create tables for user accounts and roles
// ModelBuilder : Customize the structure of the tables

// Use my User class instead of the default Identity user (here : IdentityDbContext<User>)
// We use it like this, in case we will modify and add some custom properties to User model
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comment { get; set; }

    // Set up the database schema the way I want it
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // EF Core, go ahead and do your usual setup first
        base.OnModelCreating(modelBuilder);

        // Roles : Labels for user permissions. Like admin, moderator, etc.
        // Identity Role : Represent a role in the system (like Admin, User, etc.)
        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Id = "6f8d53a2-92bb-4e91-bde4-1c5bca980e12",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },

            new IdentityRole
            {
                Id = "7c19ef1b-ded0-4a18-a2c8-3dcf68589b8b",
                Name = "User",
                NormalizedName = "USER"
            }
        ];
        
        // HasData() : When you create the database, insert these values into AspNetRoles (a table in database)
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
    }
}