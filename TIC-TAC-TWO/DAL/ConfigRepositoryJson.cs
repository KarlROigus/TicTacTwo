using System.Net.WebSockets;
using System.Text.Json;
using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{
    
    public List<string> GetConfigurationNames()
    {

        CheckAndCreateInitialConfig();

        var result = new List<string>();

        
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.ConfigExtension).ToList())
        {
            var twoParts = Path.GetFileNameWithoutExtension(fullFileName);
            var mainFileName = Path.GetFileNameWithoutExtension(twoParts);
            result.Add(mainFileName);
        }

        return result;


    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {
        
        var allConfigNames = GetConfigurationNames();
        var correctConfigName = allConfigNames[index];
        
        var configJsonStr = File.ReadAllText(ConstantlyUsed.BasePath + correctConfigName + ConstantlyUsed.ConfigExtension);
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        
        return config;
    }
    

    public void AddNewConfiguration(GameConfiguration newConfig)
    {
        if (!Directory.Exists(ConstantlyUsed.BasePath))
        {
            Directory.CreateDirectory(ConstantlyUsed.BasePath);
        }

        var gameOptionJson = JsonSerializer.Serialize(newConfig);
        File.WriteAllText(ConstantlyUsed.BasePath + newConfig.Name + ConstantlyUsed.ConfigExtension, gameOptionJson);
    }

    private void CheckAndCreateInitialConfig()
    {
        if (!Directory.Exists(ConstantlyUsed.BasePath))
        {
            Directory.CreateDirectory(ConstantlyUsed.BasePath);
        }
        
        var data = Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.ConfigExtension).ToList();

        if (data.Count == 0)
        {
            var hardcodedRepo = new ConfigRepositoryHardCoded();
            var optionNames = hardcodedRepo.GetConfigurationNames();
            for (int i = 0; i < optionNames.Count; i++)
            {
                var gameOption = hardcodedRepo.GetConfigurationByIndex(i);
                var optionJson = JsonSerializer.Serialize(gameOption);
                File.WriteAllText(ConstantlyUsed.BasePath + gameOption.Name + ConstantlyUsed.ConfigExtension, optionJson);
            }
        }
    }
}