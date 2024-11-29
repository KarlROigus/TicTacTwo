using GameBrain;

namespace DAL;

public interface IConfigRepository
{
    List<string> GetConfigurationNames(string userName);
    GameConfiguration GetConfigurationByIndex(int index, string userName);
    void AddNewConfiguration(GameConfiguration newConfig);
}