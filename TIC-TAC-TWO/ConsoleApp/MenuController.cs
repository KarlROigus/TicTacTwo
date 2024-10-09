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
            {"C", new MenuItem()
                {
                    Title = "Choose current game configuration",
                    Shortcut = "C",
                    MenuItemAction = _gameController.ChooseCurrentGameConfigurationMenu //GetConfigMenu.Run
                }
                
            },
            {"N", new MenuItem()
                {
                    Title = "New game configuration",
                    Shortcut = "N",
                    MenuItemAction = _gameController.MakeNewGameConfigurationMenu
                }
                
            }
        });

        return optionsMenu;
    }
    
}