using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class Game
{
    public void Run()
    {
        
        var mainMenu = MenuController.GetMainMenu();
        mainMenu.Run();
    }
}