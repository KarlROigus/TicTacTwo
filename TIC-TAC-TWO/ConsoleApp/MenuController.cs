using DAL;
using MenuSystem;

namespace ConsoleApp;

public class MenuController
{
    
    private GameController  _gameController = new GameController();
    public Menu GetMainMenu()
    {

        
        
        var mainMenu =  new Menu(EMenuLevel.Main, "TIC-TAC-TWO", new Dictionary<string, MenuItem>
        {
            {"O", new MenuItem()
            {
                Title = "Options",
                Shortcut = "O",
                MenuItemAction = GetOptionsMenu().Run
            }},
            {"P", new MenuItem()
            {
                Title = "Play new game",
                Shortcut = "P",
                MenuItemAction = _gameController.PlayNewGame
            }},
            {"L", new MenuItem()
            {
                Title = "Load game",
                Shortcut = "L",
                MenuItemAction = null
            }},
        });

        return mainMenu;
    }

    private Menu GetOptionsMenu()
    {
        var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO OPTIONS", new Dictionary<string, MenuItem>
        {
            {
                "C", new MenuItem()
                {
                    Title = "Change game configuration",
                    Shortcut = "C",
                    MenuItemAction = GetConfigMenu().Run //Todo: change to _gameConfiguration.Change....
                }
            },
        });

        return optionsMenu;
    }

    private static Menu GetConfigMenu()
    {
        var configRepository = new ConfigRepository();

        var configMenuItems = new Dictionary<string, MenuItem>();

        EAlphabet[] alphabetArray = (EAlphabet[])Enum.GetValues(typeof(EAlphabet));

        for (int i = 0; i < configRepository.GetConfigurationNames().Count; i++)
        {
            var newAlphabetLetter = alphabetArray[i].ToString();

            configMenuItems.Add(newAlphabetLetter, new MenuItem()
            {
                Title = configRepository.GetConfigurationNames()[i],
                Shortcut = newAlphabetLetter,
                MenuItemAction = () => newAlphabetLetter
            });
        }
        
        var configMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO Choose config", configMenuItems);

        return configMenu;
    }
}