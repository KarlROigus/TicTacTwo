using DAL;
using MenuSystem;

namespace ConsoleApp;

public static class MenuController
{
    
    
    public static Menu GetMainMenu()
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
                MenuItemAction = GameController.PlayNewGame
            }},
            {"L", new MenuItem()
            {
                Title = "Load game",
                Shortcut = "L",
                MenuItemAction = null
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

    private static Menu GetOptionsMenu()
    {
        var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO OPTIONS", new Dictionary<string, MenuItem>
        {
            {"C", new MenuItem()
                {
                    Title = "Choose current game configuration",
                    Shortcut = "C",
                    MenuItemAction = GameController.ChooseCurrentGameConfigurationMenu
                }
                
            },
            {"N", new MenuItem()
                {
                    Title = "New game configuration",
                    Shortcut = "N",
                    MenuItemAction = GameController.MakeNewGameConfigurationMenu
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
                    Shortcut = "A",
                    MenuItemAction = null,
                    ShouldReturnByItself = true
                }},
                {"B", new MenuItem()
                {
                    Title = "Move current piece",
                    Shortcut = "B",
                    MenuItemAction = null,
                    ShouldReturnByItself = true,
                }},
                {"C", new MenuItem()
                {
                    Title = "Change grid position",
                    Shortcut = "C",
                    MenuItemAction = null,
                    ShouldReturnByItself = true
                }}
            });

        return advancedGameOptionsMenu;
    }
    
}