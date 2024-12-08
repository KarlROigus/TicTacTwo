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

        if (configsThatPersonHas.Count == 0)
        {
            InsertTwoInitialConfigurations(userName);
            configsThatPersonHas = _database.Configs
                .Where(each => each.UserId == correctPersonId)
                .Select(each => each.ConfigName)
                .ToList();
        }

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

    public void AddNewConfiguration(GameConfiguration newConfig, string userName)
    {
        var userId = _database.Users.First(user => user.Username == userName).UserId;
        
        
        _database.Configs.Add(new Config()
        {
            ConfigName = newConfig.Name,
            ConfigJsonString = JsonSerializer.Serialize(newConfig),
            UserId = userId
        });
        
        _database.SaveChanges(); // CRUCIAL SENTENCE
    }

    public GameConfiguration GetConfigurationByName(string confName, string userName)
    {
        var userId = _database.Users.First(each => each.Username == userName).UserId;


        var userConfigs = _database.Configs.Where(each => each.UserId == userId);

        var chosenConf = userConfigs.First(each => each.ConfigName == confName);
        var jsonString = chosenConf.ConfigJsonString;

        return JsonSerializer.Deserialize<GameConfiguration>(jsonString);

    }

    public List<GameConfiguration> GetUserConfigurations(string userName)
    {
        var userId = _database.Users.First(each => each.Username == userName).UserId;

        var confsInDb = _database.Configs.Where(each => each.UserId == userId);

        var answer = new List<GameConfiguration>();

        foreach (var conf in confsInDb)
        {
            var deSerializedConf = JsonSerializer.Deserialize<GameConfiguration>(conf.ConfigJsonString);
            answer.Add(deSerializedConf);
        }

        return answer;
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