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
            .HasOne(c => c.PlayerX) 
            .WithMany()
            .HasForeignKey(c => c.PlayerXUserId)
            .OnDelete(DeleteBehavior.Restrict); 

        
        modelBuilder.Entity<Game>()
            .HasOne(c => c.PlayerO)
            .WithMany() 
            .HasForeignKey(c => c.PlayerOUserId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<Config>()
            .HasOne(c => c.User)
            .WithMany() 
            .HasForeignKey(c => c.UserId) 
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<User>()
            .Ignore(u => u.Configs); 
        
        modelBuilder.Entity<User>()
            .Ignore(u => u.Games); 
    }
}