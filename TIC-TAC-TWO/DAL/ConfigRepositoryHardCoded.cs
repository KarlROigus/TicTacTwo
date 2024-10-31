using GameBrain;

namespace DAL;

public class ConfigRepositoryHardCoded : IConfigRepository
{
 
    private List<GameConfiguration> _gameConfigurations =
    [
        new GameConfiguration()
        {
            Name = "Classical TIC-TAC-TOE",
            BoardDimension = 3,
            GridDimension = 3,
            WinCondition = 3,
            HowManyMovesTillAdvancedGameMoves = -1, // Cannot exist in the classical game
            Grid = new Grid(1, 1, 3, 3)

        },

        new GameConfiguration()
        {
            Name = "Default TIC-TAC-TWO"
        }


    ];

    public List<string> GetConfigurationNames()
    {
        return _gameConfigurations.Select
                (config => config.Name)
            .ToList()!; 
    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {

        return _gameConfigurations[index];
    }

    public GameConfiguration GetDefaultConfiguration()
    {
        return _gameConfigurations[1];
    }

    public void AddNewConfiguration(GameConfiguration newConfig)
    {
        _gameConfigurations.Add(newConfig);
    }


}