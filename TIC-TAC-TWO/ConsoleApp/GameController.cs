using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class GameController
{
    public static string RunGame()
    {

        var configRepository = new ConfigRepository();

        var configMenuItems = new Dictionary<string, MenuItem>();

        for (int i = 0; i < configRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = (1 + i).ToString();
            configMenuItems.Add(i.ToString(), new MenuItem()
            {
                Title = configRepository.GetConfigurationNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }

        var configMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO Choose config", configMenuItems);
        var chosenConfigShortcut = configMenu.Run();

        if (!int.TryParse(chosenConfigShortcut, out var configNo))
        {
            return chosenConfigShortcut;
        }

        var chosenConfig =
            configRepository.GetConfigurationByName(
                configRepository.GetConfigurationNames()[configNo]);
        
        var gameInstance = new TicTacTwoBrain(chosenConfig);
        

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
            {"P", new MenuItem()
            {
                Title = "Play new game",
                Shortcut = "P",
                MenuItemAction = NewGame
            }},
            {"L", new MenuItem()
            {
                Title = "Load game",
                Shortcut = "L",
                MenuItemAction = null
            }},
        });
        
        mainMenu.Run();

        return "hi";

        // ==================================

        string NewGame()
        {
            
            //Todo: implement exit method for the game.
            
            do
            {
                ConsoleUI.Visualizer.DrawBoard(gameInstance);
                Console.WriteLine("Making a move - use arrows keys to move around, press Enter to select a location.");
                Console.WriteLine("Current one to move: " + gameInstance.GetNextOneToMove());

                int boardWidth = (gameInstance.GameBoard.GetLength(0) - 1) * 4 + 1;
                int boardHeight = (gameInstance.GameBoard.GetLength(1) - 1) * 2;

                int cursorX = 1;
                int cursorY = 0;
                bool enterHasBeenPressed = false;
            
                while (!enterHasBeenPressed)
                {
                    Console.SetCursorPosition(cursorX, cursorY);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (cursorY > 0) cursorY -= 2;
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (cursorY < boardHeight) cursorY += 2;
                    }
                    else if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (cursorX > 1) cursorX -= 4;
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        if (cursorX < boardWidth) cursorX += 4;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        enterHasBeenPressed = true;
                    } 
                
                }
                
                Console.Clear();
                int indexForX = cursorX / 4;
                int indexForY = cursorY / 2;
                gameInstance.MakeAMove(indexForX, indexForY);
                
            } while (true);
            
            return "Hi";
        }


        
    }
}