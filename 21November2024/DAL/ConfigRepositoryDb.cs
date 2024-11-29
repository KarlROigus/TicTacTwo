using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ConfigRepositoryDb : IConfigRepository
{

    public AppDbContext _database;

    public ConfigRepositoryDb(AppDbContext ctx)
    {
        _database = ctx;
    }
    
    public List<string> GetConfigurationNames(string userName)
    {

        var correctPersonId = _database.Users.First(each => each.Username == userName).UserId;

        var configsThatPersonHas = _database.Configs
            .Where(each => each.UserId == correctPersonId)
            .Select(each => each.ConfigName)
            .ToList();

        return configsThatPersonHas;

    }

    public GameConfiguration GetConfigurationByIndex(int index, string userName)
    {
        var allConfigNames = GetConfigurationNames(userName);
        
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

    private void InsertTwoInitialConfigurations(string userName)
    {
        var runner = new ConfigRepositoryHardCoded();
        var userId = _database.Users.First(each => each.Username == userName).UserId;

        var classicConfig = runner.GetClassicConfiguration();
        var defaultConfig = runner.GetDefaultConfiguration();

        _database.Configs.Add(new Config()
        {
            ConfigName = "Classical TIC-TAC-TOE",
            ConfigJsonString = JsonSerializer.Serialize(classicConfig),
            UserId = userId
        });
        
        _database.Configs.Add(new Config()
        {
            ConfigName = "Default TIC-TAC-TWO",
            ConfigJsonString = JsonSerializer.Serialize(defaultConfig),
            UserId = userId
        });

        _database.SaveChanges(); // CRUCIAL SENTENCE
    }
}