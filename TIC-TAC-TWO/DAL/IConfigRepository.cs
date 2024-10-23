using GameBrain;

namespace DAL;

public interface IConfigRepository
{
   List<string> GetConfigurationNames();
   GameConfiguration GetConfigurationByIndex(int index);
   void AddNewConfiguration(GameConfiguration newConfig);
}