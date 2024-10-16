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
}