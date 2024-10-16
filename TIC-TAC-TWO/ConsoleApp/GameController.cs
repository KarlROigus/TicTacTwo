using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public static class GameController
{

    private static readonly IConfigRepository ConfigRepository = new ConfigRepositoryJson();
    private static GameConfiguration _currentGameConfiguration;
    private static bool _gameIsTerminated;

    static GameController()
    {
        _currentGameConfiguration = ConfigRepository.GetDefaultConfiguration();
    }

    
    public static string PlayNewGame()
    {
        var gameInstance = new TicTacTwoBrain(_currentGameConfiguration);
        _gameIsTerminated = false;

        Console.WriteLine(gameInstance.GameBoard.Length);
        Console.WriteLine(gameInstance.GameBoard[0].Length);
        
        Console.ReadLine();
        
        do
        {
            MakeANormalMoveWithoutAdditionalOptions(gameInstance);

            if (_gameIsTerminated)
            {
                break;
            }

            if (!gameInstance.SomebodyHasWon()) continue;
            AnnounceWinnerAndStopTheGame(gameInstance);
            break;

        } while (gameInstance.GetMovesMade() <
                 _currentGameConfiguration.HowManyMovesTillAdvancedGameMoves * 2 ||
                 _currentGameConfiguration.HowManyMovesTillAdvancedGameMoves == -1);

        if (_gameIsTerminated)
        {
            Console.Clear();
            return "r";
        }

        if (_currentGameConfiguration.HowManyMovesTillAdvancedGameMoves != -1)
        {
            do
            {
                if (_gameIsTerminated)
                {
                    break;
                }
                var advancedGameOptionsMenu = MenuController.GetAdvancedGameOptionsMenu();
                var chosenShortcut = advancedGameOptionsMenu.Run();
                
                if (chosenShortcut == MenuConstants.MoveAPieceOnTheBoardShortcut) 
                {
                    MoveAPieceOnTheBoard(gameInstance);
                } else if (chosenShortcut == MenuConstants.ChangeGridPositionShortcut)
                {
                    MoveTheGrid(gameInstance);
                    
                } else if (chosenShortcut == MenuConstants.AddANewPieceShortcut)
                {
                    MakeANormalMoveWithoutAdditionalOptions(gameInstance);
                }
                else if (chosenShortcut == MenuConstants.ExitShortcut)
                {
                    break;
                }
                if (gameInstance.SomebodyHasWon())
                {
                    AnnounceWinnerAndStopTheGame(gameInstance);
                
                    break;
                }
            
            } while (true);
        }
        
        return "finished";
    }

    private static void AnnounceWinnerAndStopTheGame(TicTacTwoBrain gameInstance)
    {
        Console.Clear();
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        Console.WriteLine();
        Console.WriteLine($"{gameInstance.GetPreviousMover()} has won the game!");
        Console.WriteLine("Press any key to return to the main menu!");
        Console.ReadLine();
        Console.Clear();
    }


    private static void MakeANormalMoveWithoutAdditionalOptions(TicTacTwoBrain gameInstance)
    {
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        Console.WriteLine("Making a move - use arrows keys to move around, press Enter to select a location.");
        Console.WriteLine("Current one to move: " + gameInstance.GetNextOneToMove());
        Console.WriteLine();
        Console.WriteLine("Press Q to QUIT.");
        int boardWidth = (gameInstance.DimX - 1) * 4 + 1;
        int boardHeight = (gameInstance.DimY - 1) * 2;

        int cursorX = 1;
        int cursorY = 0;
        bool enterHasBeenPressed = false;
        

        while (!enterHasBeenPressed)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Q)
            {
                _gameIsTerminated = true;
                break;
            }
            
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

        if (_gameIsTerminated)
        {
            return;
        }

        var indexForX = cursorX / 4;
        var indexForY = cursorY / 2;
        gameInstance.MakeAMove(indexForX, indexForY);
    }

    private static void MoveTheGrid(TicTacTwoBrain gameInstance)
    {
        
        var boardWidth = (gameInstance.DimX - 1) * 4 + 1;
        var boardHeight = (gameInstance.DimY - 1) * 2;
                
        var enterHasBeenPressed = false;
        var cursorX = 1;
        var cursorY = 0;
            
        while (!enterHasBeenPressed)
        {
            ConsoleUI.Visualizer.DrawBoard(gameInstance);
            Console.WriteLine($"{gameInstance.GetNextOneToMove()} -> choose a new center spot for the grid: ");
            
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
                var newCenterSpotX = cursorX / 4;
                var newCenterSpotY = cursorY / 2;

                if (NewCenterSpotIsValid(newCenterSpotX, newCenterSpotY, gameInstance))
                {
                    
                    gameInstance.MoveTheGrid(newCenterSpotX, newCenterSpotY);
                    enterHasBeenPressed = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You have picked an invalid spot, press Enter to pick again!");
                    Console.ReadLine();
                }
                
            }
        }
        
    }

    private static bool NewCenterSpotIsValid(int newGridMiddleSpotX, int newGridMiddleSpotY, TicTacTwoBrain gameInstance)
    {
        Console.Clear();

        var freeSpaceLeftToMove = (gameInstance.DimX - gameInstance.GetGrid().GetGridLength()) / 2;
        var currentGridMiddleSpotX = gameInstance.GetGrid().GetGridMiddleXValue();
        var currentGridMiddleSpotY = gameInstance.GetGrid().GetGridMiddleYValue();

        return Math.Abs(newGridMiddleSpotX - currentGridMiddleSpotX) <= freeSpaceLeftToMove &&
               Math.Abs(newGridMiddleSpotY - currentGridMiddleSpotY) <= freeSpaceLeftToMove;
    }

    private static void MoveAPieceOnTheBoard(TicTacTwoBrain gameInstance)
    {
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        
        Console.WriteLine($"{gameInstance.GetNextOneToMove()} -> choose a piece to move: ");
                
        int boardWidth = (gameInstance.DimX - 1) * 4 + 1;
        int boardHeight = (gameInstance.DimY - 1) * 2;
                
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
                var anotherEnterPressed = false;
                var oldSpotY = cursorY / 2;
                var oldSpotX = cursorX / 4;
                var oldSpotPicked = gameInstance.GameBoard[oldSpotY][oldSpotX];
                if (oldSpotPicked.GetSpotValue() == gameInstance.GetNextOneToMove())
                {
                    oldSpotPicked.SetSpotValue(EGamePiece.Empty);
                    
                    ConsoleUI.Visualizer.DrawBoard(gameInstance);
                    
                    Console.WriteLine($"{gameInstance.GetNextOneToMove()} -> choose a new spot: ");
                    while (!anotherEnterPressed)
                    {
                        Console.SetCursorPosition(cursorX, cursorY);
                        ConsoleKeyInfo anotherKeyInfo = Console.ReadKey(true);
                        if (anotherKeyInfo.Key == ConsoleKey.UpArrow)
                        {
                            if (cursorY > 0) cursorY -= 2;
                        }
                        else if (anotherKeyInfo.Key == ConsoleKey.DownArrow)
                        {
                            if (cursorY < boardHeight) cursorY += 2;
                        }
                        else if (anotherKeyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            if (cursorX > 1) cursorX -= 4;
                        }
                        else if (anotherKeyInfo.Key == ConsoleKey.RightArrow)
                        {
                            if (cursorX < boardWidth) cursorX += 4;
                        }
                        else if (anotherKeyInfo.Key == ConsoleKey.Enter)
                        {
                            var newSpotY = cursorY / 2;
                            var newSpotX = cursorX / 4;
                            var newSpotPicked = gameInstance.GameBoard[newSpotY][newSpotX];
                            if (newSpotPicked.GetSpotValue() == EGamePiece.Empty)
                            {
                                gameInstance.MakeAMove(newSpotX, newSpotY);
                                anotherEnterPressed = true;
                            }
                            
                        }
                    }
                    
                    enterHasBeenPressed = true;
                }
                
            }
        }
        
    }


    public static string ChooseCurrentGameConfigurationMenu()
    {

        var configMenuItems = new Dictionary<string, MenuItem>();
        
        for (var i = 0; i < ConfigRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(returnValue, new MenuItem()
            {
                Title = ConfigRepository.GetConfigurationNames()[i]!,
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue,
                ShouldReturnByItself = true,
                ChangeConfigAction = ChangeGameConfiguration
            });
        }

        var configMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO Choose config", configMenuItems);

        return configMenu.Run();

    }

    private static void ChangeGameConfiguration(string shortcut)
    {
        
        if (shortcut == MenuConstants.ExitShortcut ||  shortcut == MenuConstants.ExitShortcut || shortcut ==  MenuConstants.ReturnShortcut)
        {
            return;
        }


        if (!int.TryParse(shortcut, out var chosenShortcutIndex))
        {
            return;
        }
        
        var chosenConfig = ConfigRepository.GetConfigurationByIndex(chosenShortcutIndex);

        _currentGameConfiguration = chosenConfig;
        

        Console.WriteLine();
        Console.WriteLine("Game configuration changed succesfully! Press Enter to continue.");
        Console.ReadLine();
        Console.WriteLine();

        return;
    }


    public static string MakeNewGameConfigurationMenu()
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
            Grid = new Grid(boardWidth / 2, boardWidth / 2, boardWidth, gridWidth)

        };

        ConfigRepository.AddNewConfiguration(newGameConfiguration);
        
        Console.WriteLine();
        Console.WriteLine("New configuration added successfully! Now go on and choose your configuration!" +
                          "Press Enter to continue.");
        Console.ReadLine();
        Console.WriteLine();
        return "r";
    }

    private static int GetNewBoardWidth()
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

    private static string GetNewConfigName()
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
    
    private static int GetNewBoardHeight()
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
    
    private static int GetNewGridWidth(int boardWidth)
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
            } else if (boardHeightInteger > boardWidth)
            {
                Console.WriteLine("Grid cannot be wider than the main board! ");
            } else if (boardHeightInteger < 3)
            {
                Console.WriteLine("Please insert a number that is greater than 2 for the game to make sense! ");
            } else
            {
                return boardHeightInteger;
            }
        } while (true);
        
    }

    private static int GetWinCondition()
    {
        Console.Write("Please insert how many same symbols in a row needed to win: ");
        var winCondition = Console.ReadLine();
        return int.Parse(winCondition);
    }

    private static int GetMovesNeededTillAdvancedMoves()
    {
        Console.Write("Please insert after how many moves advanced moving options apply: ");
        var howManyMovesTillAdvancedMoves = Console.ReadLine();
        return int.Parse(howManyMovesTillAdvancedMoves);
    }
    
    
    
    // =========================================================
}