using Domain;
using Microsoft.EntityFrameworkCore;
using GameState = GameBrain.GameState;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<Config> Configs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<State> States { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the first relationship (PrimaryUser)
        modelBuilder.Entity<Game>()
            .HasOne(c => c.PlayerX) // Navigation property in Config
            .WithMany() // No navigation property in User
            .HasForeignKey(c => c.PlayerXUserId) // Foreign key in Config
            .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete

        // Configure the second relationship (SecondaryUser)
        modelBuilder.Entity<Game>()
            .HasOne(c => c.PlayerO) // Navigation property in Config
            .WithMany() // No navigation property in User
            .HasForeignKey(c => c.PlayerOUserId) // Foreign key in Config
            .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascade delete
        
        modelBuilder.Entity<User>()
            .Ignore(u => u.Configs); 
        
        modelBuilder.Entity<User>()
            .Ignore(u => u.Games); 
    }
}