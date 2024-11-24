using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<Config> Configs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameState> GameStates { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the first relationship (PrimaryUser)
        modelBuilder.Entity<Game>()
            .HasOne(c => c.PrimaryUser) // Navigation property in Config
            .WithMany() // No navigation property in User
            .HasForeignKey(c => c.PrimaryUserId) // Foreign key in Config
            .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        // Configure the second relationship (SecondaryUser)
        modelBuilder.Entity<Game>()
            .HasOne(c => c.SecondaryUser) // Navigation property in Config
            .WithMany() // No navigation property in User
            .HasForeignKey(c => c.SecondaryUserId) // Foreign key in Config
            .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
        
        modelBuilder.Entity<User>()
            .Ignore(u => u.Configs); 
        
        modelBuilder.Entity<User>()
            .Ignore(u => u.Games); 
    }
    
    
}