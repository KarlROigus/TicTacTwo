using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ConfigRepositoryDb : IConfigRepository
{

    public AppDbContext _context;

    public ConfigRepositoryDb()
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

        return _context.Configs.Select(config => config.Name).ToList();
    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {
        var allConfigNames = GetConfigurationNames();
        
        var correctConfigName = allConfigNames[index];

        var configJsonString = _context.Configs
            .Where(x => x.Name == correctConfigName)
            .Select(x => x.ConfigJsonString)
            .FirstOrDefault();

        if (configJsonString != null)
        {
            return JsonSerializer.Deserialize<GameConfiguration>(configJsonString);
        }

        throw new Exception("Should not happen"); // Should never happen
    }

    public void AddNewConfiguration(GameConfiguration newConfig)
    {
        _context.Configs.Add(new Config()
        {
            Name = newConfig.Name,
            ConfigJsonString = JsonSerializer.Serialize(newConfig)
        });
        
        _context.SaveChanges(); // CRUCIAL SENTENCE
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