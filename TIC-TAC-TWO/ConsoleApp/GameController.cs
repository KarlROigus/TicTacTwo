using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class GameController
{

    private readonly ConfigRepository _configRepository;
    private GameConfiguration _currentGameConfiguration;

    public GameController()
    {
        _configRepository = new ConfigRepository();
        _currentGameConfiguration = _configRepository.GetDefaultConfiguration();
    }

    
    public string PlayNewGame()
    {
        var gameInstance = new TicTacTwoBrain(_currentGameConfiguration);

        do
        {
            ConsoleUI.Visualizer.DrawBoard(gameInstance);
            Console.WriteLine("Making a move - use arrows keys to move around, press Enter to select a location.");
            Console.WriteLine("Current one to move: " + gameInstance.GetNextOneToMove());
            MakeANormalMoveWithoutAdditionalOptions(gameInstance);
            
            if (gameInstance.SomebodyHasWon())
            {
                AnnounceWinnerAndStopTheGame(gameInstance);
                break;
            }

        } while (gameInstance.GetMovesMade() <
                   _currentGameConfiguration.HowManyMovesTillAdvancedGameMoves * 2 ||
                   _currentGameConfiguration.HowManyMovesTillAdvancedGameMoves == -1);

        if (_currentGameConfiguration.HowManyMovesTillAdvancedGameMoves != -1)
        {
            do
            {
                var advancedGameOptionsMenu = new MenuController().GetAdvancedGameOptionsMenu();
                var chosenShortcut = advancedGameOptionsMenu.Run();
                if (chosenShortcut == "M") 
                {
                    MoveAPieceOnTheBoard(gameInstance);
                
                } else if (chosenShortcut == "C")
                {
                    // Let them choose a new centre spot for the grid
                    // If allowed, all good
                } else if (chosenShortcut == "A")
                {
                    //Continue with normal gameplay
                }
                if (gameInstance.SomebodyHasWon())
                {
                    Console.Clear();
                    ConsoleUI.Visualizer.DrawBoard(gameInstance);
                    Console.WriteLine();
                    Console.WriteLine($"{gameInstance.GetPreviousMover()} has won the game!");
                    Console.WriteLine("Press any key to return to the main menu!");
                    Console.ReadLine();
                    Console.Clear();
                
                    break;
                }
            
            } while (true);
        }
        
        return "finished";
    }

    private void AnnounceWinnerAndStopTheGame(TicTacTwoBrain gameInstance)
    {
        Console.Clear();
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        Console.WriteLine();
        Console.WriteLine($"{gameInstance.GetPreviousMover()} has won the game!");
        Console.WriteLine("Press any key to return to the main menu!");
        Console.ReadLine();
        Console.Clear();
    }


    private void MakeANormalMoveWithoutAdditionalOptions(TicTacTwoBrain gameInstance)
    {
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

        var indexForX = cursorX / 4;
        var indexForY = cursorY / 2;
        gameInstance.MakeAMove(indexForX, indexForY);
    }

    private void MoveAPieceOnTheBoard(TicTacTwoBrain gameInstance)
    {
        ConsoleUI.Visualizer.DrawBoard(gameInstance);

        Console.WriteLine($"{gameInstance.GetNextOneToMove()} -> choose a piece to move: ");
                
        int boardWidth = (gameInstance.GameBoard.GetLength(0) - 1) * 4 + 1;
        int boardHeight = (gameInstance.GameBoard.GetLength(1) - 1) * 2;
                
        bool enterHasBeenPressed = false;
        int cursorX = 1;
        int cursorY = 0;
            
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
    }

    public string ChooseCurrentGameConfigurationMenu()
    {

        var configMenuItems = new Dictionary<string, MenuItem>();

        EAlphabet[] alphabetArray = (EAlphabet[])Enum.GetValues(typeof(EAlphabet));

        for (int i = 0; i < _configRepository.GetConfigurationNames().Count; i++)
        {
            var newAlphabetLetter = alphabetArray[i].ToString();

            configMenuItems.Add(newAlphabetLetter, new MenuItem()
            {
                Title = _configRepository.GetConfigurationNames()[i] ?? "",
                Shortcut = newAlphabetLetter,
                MenuItemAction = () => newAlphabetLetter,
                ChangeConfigAction = ChangeGameConfiguration
            });
        }
        
        var configMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO Choose config", configMenuItems);

        var chosenConfigShortcut = configMenu.Run();

        if (chosenConfigShortcut == "E" || chosenConfigShortcut == "M" || chosenConfigShortcut == "R")
        {
            return chosenConfigShortcut;
        }
        
        if (!Enum.TryParse(chosenConfigShortcut, out EAlphabet chosenShortcutEnum))
        {
            return chosenConfigShortcut;
        }

        var chosenShortcutIndex = (int)chosenShortcutEnum;
        

        var chosenConfig = _configRepository.GetConfigurationByIndex(chosenShortcutIndex);

        _currentGameConfiguration = chosenConfig;


        return chosenConfigShortcut;
    }

    private void ChangeGameConfiguration(string shortcut)
    {
        
        if (shortcut is "E" or "M" or "R")
        {
            return;
        }
        
        if (!Enum.TryParse(shortcut, out EAlphabet chosenShortcutEnum))
        {
            return;
        }

        var chosenShortcutIndex = (int)chosenShortcutEnum;
        
        
        var chosenConfig = _configRepository.GetConfigurationByIndex(chosenShortcutIndex);

        _currentGameConfiguration = chosenConfig;
        

        Console.WriteLine();
        Console.WriteLine("Game configuration changed succesfully!");
        Console.WriteLine();

        return;
    }


    public string MakeNewGameConfigurationMenu()
    {
        
        Console.Clear();
        Console.WriteLine();
        var name = GetNewConfigName();
        var boardWidth = GetNewBoardWidth();
        var boardHeight = GetNewBoardHeight();
        var gridWidth = GetNewGridWidth(boardWidth);
        var winCondition = GetWinCondition();
        var howManyMovesTillAdvancedMoves = GetMovesNeededTillAdvancedMoves();
        
        var newGameConfiguration = new GameConfiguration()
        {
            Name = name,
            BoardWidth = boardWidth,
            BoardHeight = boardHeight,
            GridHeight = gridWidth,
            GridWidth = gridWidth,
            WinCondition = winCondition,
            HowManyMovesTillAdvancedGameMoves = howManyMovesTillAdvancedMoves,
            Grid = new Grid(boardWidth, boardWidth / 2, boardWidth / 2, gridWidth)

        };

        _configRepository.AddNewConfiguration(newGameConfiguration);

        Console.WriteLine();
        Console.WriteLine("HELLO WORLD!");
        return "hi";
    }

    private int GetNewBoardWidth()
    {
        do
        {
            Console.Write("Please insert board width: ");
            var boardWidth = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(boardWidth))
            {
                Console.WriteLine("You must insert something! ");
            }
            else if (!int.TryParse(boardWidth, out var boardWidthInteger))
            {
                Console.WriteLine("Please insert a valid number! You inserted unknown symbol! ");
            } else if (boardWidthInteger < 3)
            {
                Console.WriteLine("Please insert a number that is greater than 2 for the game to make sense!");
            }
            else
            {
                return boardWidthInteger;
            }
            
        } while (true);
        
    }

    private string GetNewConfigName()
    {

        do
        {
            Console.Write("Please give a name for new configuration: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("You must insert something!");
            }
            else
            {
                return name;
            }
            
        } while (true);
    }
    
    private int GetNewBoardHeight()
    {
        do
        {
            Console.Write("Please insert board height: ");
            var boardHeight = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(boardHeight))
            {
                Console.WriteLine("You must insert something! ");
            }
            else if (!int.TryParse(boardHeight, out var boardHeightInteger))
            {
                Console.WriteLine("Please insert a valid number! You inserted unknown symbol! ");
            } else if (boardHeightInteger < 3)
            {
                Console.WriteLine("Please insert a number that is greater than 2 for the game to make sense! ");
            }
            else
            {
                return boardHeightInteger;
            }
        } while (true);
        
    }
    
    private int GetNewGridWidth(int boardWidth)
    {
        do
        {
            Console.Write($"Please insert grid width. Grid can not be wider than {boardWidth} and has to be an odd number: ");
            var gridWidth = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(gridWidth))
            {
                Console.WriteLine("You must insert something!");
            }
            else if (!int.TryParse(gridWidth, out var boardHeightInteger))
            {
                Console.WriteLine("Please insert a valid number! You inserted unknown symbol! ");
            } else if (boardHeightInteger % 2 == 0) 
            {
                Console.WriteLine("Please insert an odd number for the game to make sense! ");
            } else if (boardHeightInteger >= boardWidth)
            {
                Console.WriteLine("Grid cannot be wider than the main board! ");
            } else
            {
                return boardHeightInteger;
            }
        } while (true);
        
    }

    private int GetWinCondition()
    {
        Console.Write("Please insert how many same symbols in a row needed to win: ");
        var winCondition = Console.ReadLine();
        return int.Parse(winCondition);
    }

    private int GetMovesNeededTillAdvancedMoves()
    {
        Console.Write("Please insert after how many moves advanced moving options apply: ");
        var howManyMovesTillAdvancedMoves = Console.ReadLine();
        return int.Parse(howManyMovesTillAdvancedMoves);
    }
    
    
    // =========================================================
}