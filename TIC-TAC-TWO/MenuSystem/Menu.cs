
using DAL;


namespace MenuSystem;

public class Menu
{
    private string MenuHeader { get; set; }
    private static string _menuDivider = "============";
    
    private Dictionary<string, MenuItem> DictMenuItems{ get; set; }

    private MenuItem _menuItemExit = new MenuItem()
    {
        Shortcut = ConstantlyUsed.ExitShortcut,
        Title = "Exit"
    };
    private MenuItem _menuItemReturn = new MenuItem()
    {
        Shortcut = ConstantlyUsed.ReturnShortcut,
        Title = "Return"
    };
    private MenuItem _menuItemReturnMain = new MenuItem()
    {
        Shortcut = ConstantlyUsed.ReturnToMainMenuShortcut,
        Title = "Return to Main menu"
    };
    private EMenuLevel MenuLevel { get; set; }

    public Menu(EMenuLevel menuLevel, string menuHeader, Dictionary<string, MenuItem> dictMenuItems)
    {
        if (dictMenuItems == null  || dictMenuItems.Count == 0)
        {
            throw new ArgumentException("Dict menu items cannot be empty!!");
        }

        DictMenuItems = dictMenuItems;
        
        if (string.IsNullOrWhiteSpace(menuHeader))
        {
            throw new ArgumentException("Menu header cannot be empty!");
        }
        MenuHeader = menuHeader;
        
        
        MenuLevel = menuLevel;
        
        
        if (MenuLevel != EMenuLevel.Main && MenuLevel != EMenuLevel.InTheGame)
        {
            DictMenuItems.Add(ConstantlyUsed.ReturnShortcut, _menuItemReturn);
        }
        
        if (MenuLevel == EMenuLevel.Deep)
        {
            DictMenuItems.Add(ConstantlyUsed.ReturnToMainMenuShortcut, _menuItemReturnMain);
        }
        
        DictMenuItems.Add(ConstantlyUsed.ExitShortcut, _menuItemExit);

    }

    public string Run()
    {
        Console.Clear();

        do
        {
            
            var menuItem = DisplayMenuGetUserChoice();
            var menuReturnValue = "";
            
            if (menuItem.MenuItemAction != null)
            {
                menuReturnValue = menuItem.MenuItemAction();
                if (menuItem.ShouldReturnByItself)
                {
                    return menuReturnValue;
                }
            }
            
            if (menuItem.Shortcut == _menuItemReturn.Shortcut)
            {
                return _menuItemReturn.Shortcut;
            }
            
            if (menuItem.Shortcut == _menuItemExit.Shortcut || menuReturnValue == _menuItemExit.Shortcut)
            {
                return _menuItemExit.Shortcut;
            }
            
            if ((menuItem.Shortcut == _menuItemReturnMain.Shortcut || menuReturnValue == _menuItemReturnMain.Shortcut) && MenuLevel != EMenuLevel.Main)
            {
                return _menuItemReturnMain.Shortcut;
            }
            
            
        } while (true);
    }


    private MenuItem DisplayMenuGetUserChoice()
    {
        var userInput = "";

        do
        {
            DrawMenu();
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("It would be nice if u actually choose something! Try again!");
                Console.WriteLine();
            }
            else
            {
                
                if (int.TryParse(userInput, out int gameConfigurationIndex))
                {
                    userInput = (gameConfigurationIndex - 1).ToString();
                }
                else
                {
                    userInput = userInput.ToUpper();
                }
                
                foreach (var dictMenuItem in DictMenuItems)
                {
                    if (dictMenuItem.Key.ToUpper() == userInput)
                    {
                        return dictMenuItem.Value;
                    }
                }
                
                Console.WriteLine("Try to choose something from the existing options");
                Console.WriteLine();
            }
            
        } while (true);
    }
    
    private void DrawMenu()
    {
        Console.WriteLine(MenuHeader);
        Console.WriteLine(_menuDivider);

        foreach (KeyValuePair<string,MenuItem> dictMenuItem in DictMenuItems)
        {
            Console.WriteLine(dictMenuItem.Value);
        }
        
        Console.WriteLine();
        Console.Write(">");
    }
}