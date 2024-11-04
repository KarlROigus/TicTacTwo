using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ConfigRepositoryDB : IConfigRepository
{

    public AppDbContext _context;

    public ConfigRepositoryDB()
    {
        var connectionString = $"Data Source={ConstantlyUsed.BasePath}app.db";
        
        
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        _context = new AppDbContext(contextOptions);
        
    }
    
    
    public List<string> GetConfigurationNames()
    {
        if (!_context.Configs.Any())
        {
            InsertTwoInitialConfigurations();
        }

        return new List<string>();
    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {
        throw new NotImplementedException();
    }

    public void AddNewConfiguration(GameConfiguration newConfig)
    {
        throw new NotImplementedException();
    }

    private void InsertTwoInitialConfigurations()
    {
        var runner = new ConfigRepositoryHardCoded();

        var classicConfig = runner.GetClassicConfiguration();
        var defaultConfig = runner.GetDefaultConfiguration();

        _context.Configs.Add(new Config()
        {
            Name = "Classical TIC-TAC-TOE",
            ConfigJsonString = JsonSerializer.Serialize(classicConfig)
        });
        
        _context.Configs.Add(new Config()
        {
            Name = "Default TIC-TAC-TWO",
            ConfigJsonString = JsonSerializer.Serialize(defaultConfig)
        });

        _context.SaveChanges(); // CRUCIAL SENTENCE
    }
}