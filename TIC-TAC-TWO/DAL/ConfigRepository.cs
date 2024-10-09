using GameBrain;

namespace DAL;

public class ConfigRepository
{

    private List<GameConfiguration> _gameConfigurations =
    [
        new GameConfiguration()
        {
            Name = "Classical TIC-TAC-TOE",
            BoardWidth = 3,
            BoardHeight = 3,
            GridWidth = 3,
            GridHeight = 3,
            WinCondition = 3,
            HowManyMovesTillAdvancedGameMoves = 0,
            Grid = new Grid(3, 1, 1, 3)

        },

        new GameConfiguration()
        {
            Name = "Default TIC-TAC-TWO"
        }


    ];

    public List<string?> GetConfigurationNames()
    {
        return _gameConfigurations.Select
            (config => config.Name)
            .ToList();
    }

    public GameConfiguration GetConfigurationByIndex(int index)
    {

        return _gameConfigurations[index];
    }

    public GameConfiguration GetDefaultConfiguration()
    {
        return _gameConfigurations[1];
    }

    public bool AddNewConfiguration(GameConfiguration newConfig)
    {
        _gameConfigurations.Add(newConfig);
        foreach (var config in _gameConfigurations)
        {
            Console.WriteLine(config);
        }
        return true;
    }


}