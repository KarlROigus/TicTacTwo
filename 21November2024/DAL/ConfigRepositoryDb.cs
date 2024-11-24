using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ConfigRepositoryDb : IConfigRepository
{

    public AppDbContext _database;

    public ConfigRepositoryDb()
    {
        var connectionString = $"Data Source=/Users/karlrudolf/Desktop/21novStart/app.db";
        
        
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        _database = new AppDbContext(contextOptions);
        
    }
    
    public List<string> GetConfigurationNames()
    {
        if (!_database.Configs.Any())
        {
            InsertTwoInitialConfigurations();
        }
        
        return _database.Configs.Select(config => config.ConfigName).ToList();
    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {
        var allConfigNames = GetConfigurationNames();
        
        var correctConfigName = allConfigNames[index];

        var configJsonString = _database.Configs
            .Where(x => x.ConfigName == correctConfigName)
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
        _database.Configs.Add(new Config()
        {
            ConfigName = newConfig.Name,
            ConfigJsonString = JsonSerializer.Serialize(newConfig)
        });
        
        _database.SaveChanges(); // CRUCIAL SENTENCE
    }

    private void InsertTwoInitialConfigurations()
    {
        var runner = new ConfigRepositoryHardCoded();

        var classicConfig = runner.GetClassicConfiguration();
        var defaultConfig = runner.GetDefaultConfiguration();

        _database.Configs.Add(new Config()
        {
            ConfigName = "Classical TIC-TAC-TOE",
            ConfigJsonString = JsonSerializer.Serialize(classicConfig)
        });
        
        _database.Configs.Add(new Config()
        {
            ConfigName = "Default TIC-TAC-TWO",
            ConfigJsonString = JsonSerializer.Serialize(defaultConfig)
        });

        _database.SaveChanges(); // CRUCIAL SENTENCE
    }
}