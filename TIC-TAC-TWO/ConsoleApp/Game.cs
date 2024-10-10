using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class Game
{
    public void Run()
    {
        
        var mainMenu = new MenuController().GetMainMenu();
        mainMenu.Run();
    }
}