using GameBrain;

namespace DAL;

public interface IConfigRepository
{
   List<string> GetConfigurationNames();
   GameConfiguration GetConfigurationByIndex(int index);
   public GameConfiguration GetDefaultConfiguration();
   void AddNewConfiguration(GameConfiguration newConfig);
}