using System.Net.WebSockets;
using System.Text.Json;
using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{

    private readonly string _basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        + Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;
    
    public List<string> GetConfigurationNames()
    {

        CheckAndCreateInitialConfig();

        var result = new List<string>();

        foreach (var fullFileName in Directory.GetFiles(_basePath, "*.config.json").ToList())
        {
            var twoParts = Path.GetFileNameWithoutExtension(fullFileName);
            var mainFileName = Path.GetFileNameWithoutExtension(twoParts);
            result.Add(mainFileName);
        }

        return result;


    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {
        // return new GameConfiguration();


        var allConfigNames = GetConfigurationNames();
        var correctConfigName = allConfigNames[index];
        
        var configJsonStr = File.ReadAllText(_basePath + correctConfigName + ".config.json");
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        
        return config;
    }

    public GameConfiguration GetDefaultConfiguration()
    {
        return new GameConfiguration();
    }

    public void AddNewConfiguration(GameConfiguration newConfig)
    {
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }

        var gameOptionJson = JsonSerializer.Serialize(newConfig);
        File.WriteAllText(_basePath + newConfig.Name + ".config.json", gameOptionJson);
    }

    private void CheckAndCreateInitialConfig()
    {
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
        
        var data = Directory.GetFiles(_basePath, "*.config.json").ToList();

        if (data.Count == 0)
        {
            var hardcodedRepo = new ConfigRepositoryHardCoded();
            var optionNames = hardcodedRepo.GetConfigurationNames();
            for (int i = 0; i < optionNames.Count; i++)
            {
                var gameOption = hardcodedRepo.GetConfigurationByIndex(i);
                var optionJson = JsonSerializer.Serialize(gameOption);
                File.WriteAllText(_basePath + gameOption.Name + ".config.json", optionJson);
            }
        }
    }
}