using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class GameController
{
    public static void RunGame()
    {
        var gameInstance = new TicTacTwoBrain(7);

        var changeGameRulesMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO Change rules", new Dictionary<string, MenuItem>                       
        {                                                                                                                                      
            {"G", new MenuItem()                                                                                                               
            {                                                                                                                                  
                Title = "Change GRID size (default is 3x3)",                                                                                   
                Shortcut = "G",                                                                                                                
                MenuItemAction = gameInstance.ChangeGridSize                                                                                             
            }},                                                                                                                                
                                                                                                                                               
            {"B", new MenuItem()                                                                                                               
            {                                                                                                                                  
                Title = "Change BOARD size (default is 5x5)",                                                                                  
                Shortcut = "B",                                                                                                                
                MenuItemAction = null                                                                                                          
            }},                                                                                                                                
            {"P", new MenuItem()                                                                                                               
            {                                                                                                                                  
                Title = "Change amount of PIECES (default is 13 each)",                                                                        
                Shortcut = "P",                                                                                                                
                MenuItemAction = null                                                                                                          
            }}                                                                                                                                 
        });                                                                                                                                    
        

        var optionsMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO OPTIONS", new Dictionary<string, MenuItem>
        {
            {"C", new MenuItem()
            {
                Title = "Change game options",
                Shortcut = "C",
                MenuItemAction = changeGameRulesMenu.Run
            }},
        });


        var mainMenu = new Menu(EMenuLevel.Main, "TIC-TAC-TWO", new Dictionary<string, MenuItem>
        {
            {"O", new MenuItem()
            {
                Title = "Options",
                Shortcut = "O",
                MenuItemAction = optionsMenu.Run
            }},
            {"N", new MenuItem()
            {
                Title = "New game",
                Shortcut = "N",
                MenuItemAction = NewGame
            }}
        });
        
        mainMenu.Run();

        return;

        // ==================================

        string NewGame()
        {
            
            ConsoleUI.Visualizer.DrawBoard(gameInstance);
            
            return "Hi";
        }


        
    }
}