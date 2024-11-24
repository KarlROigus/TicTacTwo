using DAL;
using MenuSystem;

namespace ConsoleApp;

public class MenuController
{
    
    private string _userName;
    private static GameController _gameController;

    public MenuController(string userName)
    {
        _userName = userName;
        _gameController = new GameController(_userName);
    }
    
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
            {"M", new MenuItem()
            {
                Title = "Make new game",
                Shortcut = "M",
                MenuItemAction = _gameController.ChooseGameType
            }},
            {"J", new MenuItem()
            {
                Title = "Join a random game",
                Shortcut = "J",
                MenuItemAction = _gameController.JoinRandomGame
            }},
            {"L", new MenuItem()
            {
                Title = "Load saved game",
                Shortcut = "L",
                MenuItemAction = LoadSavedGamesMenu().Run
            }},
            {"S", new MenuItem()
            {
                Title = "Rules & Description",
                Shortcut = "S",
                MenuItemAction = ShowRulesAndDescriptionText
            }},
        });

        return mainMenu;
    }
    
    private static Menu LoadSavedGamesMenu()
    {
        var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Choose option", new Dictionary<string, MenuItem>
        {
            {"L", new MenuItem()
                {
                    Title = "Load a saved game and play it",
                    Shortcut = "L",
                    MenuItemAction = _gameController.PlayLoadedGame
                }
                
            },
        });

        return optionsMenu;
    }

    private static Menu GetOptionsMenu()
    {
        var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO OPTIONS", new Dictionary<string, MenuItem>
        {
            {"C", new MenuItem()
                {
                    Title = "Choose current game configuration",
                    Shortcut = "C",
                    MenuItemAction = _gameController.ChooseCurrentGameConfigurationMenu
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

    private static string ShowRulesAndDescriptionText()
    {
        
        Console.Clear();
        Console.WriteLine("This is TIC-TAC-TWO, a game derived from a famous game tic-tac-toe");
        Console.WriteLine("We hope you enjoyed it");


        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadLine();
        
        return "text shown";
    }


    public static Menu GetAdvancedGameOptionsMenu()
    {
        
        var advancedGameOptionsMenu = new Menu(EMenuLevel.InTheGame, "TIC-TAC-TWO Choose turn option",
            new Dictionary<string, MenuItem>()
            {
                {"A", new MenuItem()
                {
                    Title = "Add new piece",
                    Shortcut = ConstantlyUsed.AddANewPieceShortcut,
                    MenuItemAction = () => ConstantlyUsed.AddANewPieceShortcut,
                    ShouldReturnByItself = true
                }},
                {"B", new MenuItem()
                {
                    Title = "Move current piece",
                    Shortcut = ConstantlyUsed.MoveAPieceOnTheBoardShortcut,
                    MenuItemAction = () => ConstantlyUsed.MoveAPieceOnTheBoardShortcut,
                    ShouldReturnByItself = true,
                }},
                {"C", new MenuItem()
                {
                    Title = "Change grid position",
                    Shortcut = ConstantlyUsed.ChangeGridPositionShortcut,
                    MenuItemAction = () => ConstantlyUsed.ChangeGridPositionShortcut,
                    ShouldReturnByItself = true
                }}
            });

        return advancedGameOptionsMenu;
    }
    
    
}