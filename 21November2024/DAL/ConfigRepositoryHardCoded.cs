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
            PiecesPerPlayer = 3,
            HowManyMovesTillAdvancedGameMoves = -1, // Cannot exist in the classical game
            Grid = new Grid(1, 1, 3, 3)

        },

        new GameConfiguration()
        {
            Name = "Default TIC-TAC-TWO"
        }


    ];

    public List<string> GetConfigurationNames(string userName)
    {
        return _gameConfigurations.Select
                (config => config.Name)
            .ToList()!; 
    }

    public GameConfiguration GetConfigurationByIndex(int index, string userName)
    {

        return _gameConfigurations[index];
    }
    
    public GameConfiguration GetClassicConfiguration()
    {
        return _gameConfigurations[0];
    }

    public GameConfiguration GetDefaultConfiguration()
    {
        return _gameConfigurations[1];
    }

    public void AddNewConfiguration(GameConfiguration newConfig, string userName)
    {
        _gameConfigurations.Add(newConfig);
    }

    public GameConfiguration GetConfigurationByName(string confName, string userName)
    {
        throw new NotImplementedException();
    }

    public List<GameConfiguration> GetUserConfigurations(string userName)
    {
        throw new NotImplementedException();
    }
}