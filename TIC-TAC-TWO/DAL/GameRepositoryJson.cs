using System.Text.Json;
using GameBrain;

namespace DAL;


public class GameRepositoryJson : IGameRepository
{
    
    
    public void SaveGame(string jsonStateString, string gameConfigName)
    {
        
        var fileName = ConstantlyUsed.BasePath + 
                       gameConfigName + 
                       " " + 
                       DateTime.Now.ToString("O") + 
                       ConstantlyUsed.GameExtension;
        
        File.WriteAllText(fileName, jsonStateString);
    }

    public List<string> GetSavedGameNames()
    {
        var result = new List<string>();
        
        foreach (var fullFileName in Directory.GetFiles(ConstantlyUsed.BasePath, "*" + ConstantlyUsed.GameExtension).ToList())
        {
            var twoParts = Path.GetFileNameWithoutExtension(fullFileName);
            var mainFileName = Path.GetFileNameWithoutExtension(twoParts);
            result.Add(mainFileName);
        }

        return result;
    }

    public GameState GetGameStateByIndex(int index)
    {
        var allSavedGameNames = GetSavedGameNames();
        var correctSavedGameName = allSavedGameNames[index];
        
        var gameJsonStr = File.ReadAllText(ConstantlyUsed.BasePath + correctSavedGameName + ConstantlyUsed.GameExtension);
        var state = JsonSerializer.Deserialize<GameState>(gameJsonStr);

        return state!;
    }
}