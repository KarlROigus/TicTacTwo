using System.Text;
using System.Text.Json;
using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;

public class GameController
{

    private static readonly IConfigRepository ConfigRepository = new ConfigRepositoryDB();
    private static readonly IGameRepository GameRepository = new GameRepositoryJson();
    private static GameConfiguration _currentGameConfiguration = new GameConfiguration();
    private static bool _gameIsTerminated;


    public string PlayLoadedGame()
    {
        
        var chosenShortcut = GetChosenShortcutFromSavedGamesList();
        
        if (ChosenShortcutIsExitOrReturn(chosenShortcut))
        {
            return ConstantlyUsed.ReturnShortcut;
        }
        
        var chosenState = GameRepository.GetGameStateByIndex(int.Parse(chosenShortcut));
        
        Console.WriteLine("Game chosen successfully! Press any key to continue the game!");
        Console.ReadLine();
        
        var loadedGameInstance = new TicTacTwoBrain(chosenState);

        _currentGameConfiguration = chosenState.GameConfiguration;
        
        CommonGameLoop(loadedGameInstance);

        return ConstantlyUsed.ReturnToMainMenuShortcut;
    }

    public string DeleteSavedGame()
    {

        var chosenShortcut = GetChosenShortcutFromSavedGamesList();

        if (ChosenShortcutIsExitOrReturn(chosenShortcut))
        {
            return ConstantlyUsed.ReturnShortcut;
        }
        
        GameRepository.DeleteSavedGame(int.Parse(chosenShortcut) - 1);
        
        Console.WriteLine("Saved game deleted succesfully! Press any key to continue!");
        Console.ReadLine();
        
        return ConstantlyUsed.ReturnShortcut;
    }

    private string GetChosenShortcutFromSavedGamesList()
    {
        var savedGameMenuItems = new Dictionary<string, MenuItem>();
        for (var i = 0; i < GameRepository.GetSavedGameNames().Count; i++)
        {
            var returnValue = i.ToString();
            savedGameMenuItems.Add(returnValue, new MenuItem()
            {
                Title = GameRepository.GetSavedGameNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue,
                ShouldReturnByItself = true,
            });
        }

        if (!savedGameMenuItems.Any())
        {
            Console.Clear();
            Console.WriteLine("Cannot load or delete any game as there are not any games saved! Press any key to return");
            Console.ReadLine();
            return ConstantlyUsed.ReturnShortcut;
        }
        var savedGamesMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Choose a game you saved", savedGameMenuItems);

        return savedGamesMenu.Run();
    }

    private bool ChosenShortcutIsExitOrReturn(string shortcut)
    {
        return shortcut == ConstantlyUsed.ExitShortcut || shortcut == ConstantlyUsed.ReturnShortcut;
    }
    
    
    public string PlayNewGame()
    {
        var gameInstance = new TicTacTwoBrain(GetFreshGameState(_currentGameConfiguration));
        
        CommonGameLoop(gameInstance);
        
        return ConstantlyUsed.ReturnShortcut;
    }


    private GameState GetFreshGameState(GameConfiguration currentConfig)
    {
        var grid = currentConfig.Grid;

        var gameBoard = currentConfig.GetFreshGameBoard(currentConfig, grid);

        return new GameState(grid,
            gameBoard,
            currentConfig,
            0,
            EGamePiece.X,
            currentConfig.PiecesPerPlayer,
            currentConfig.PiecesPerPlayer);
    }
    
    
    private void CommonGameLoop(TicTacTwoBrain gameInstance)
    {
        _gameIsTerminated = false;
        
        do
        {
            MakeANormalMoveWithoutAdditionalOptions(gameInstance);

            if (_gameIsTerminated)
            {
                break;
            }

            if (gameInstance.SomebodyHasWon())
            {
                ConsoleUI.Visualizer.AnnounceTheWinner(gameInstance);
                _gameIsTerminated = true;
                break; 
            }
            
            if (!gameInstance.ItsADraw()) continue;
            ConsoleUI.Visualizer.AnnounceTheDraw(gameInstance);
            _gameIsTerminated = true;
            break;
            
            
        } while (gameInstance.GetMovesMade() <
                 _currentGameConfiguration.HowManyMovesTillAdvancedGameMoves * 2 ||
                 _currentGameConfiguration.HowManyMovesTillAdvancedGameMoves == ConstantlyUsed.ClassicalGame);

        if (_gameIsTerminated)
        {
            Console.Clear();
            return;
        }

        if (_currentGameConfiguration.HowManyMovesTillAdvancedGameMoves != ConstantlyUsed.ClassicalGame)
        {
            do
            {
                if (_gameIsTerminated)
                {
                    break;
                }

                var advancedGameOptionsMenu = MenuController.GetAdvancedGameOptionsMenu();
                
                var chosenShortcut = advancedGameOptionsMenu.Run();
                
                if (chosenShortcut == ConstantlyUsed.MoveAPieceOnTheBoardShortcut) 
                {
                    MoveAPieceOnTheBoard(gameInstance);
                } else if (chosenShortcut == ConstantlyUsed.ChangeGridPositionShortcut)
                {
                    MoveTheGrid(gameInstance);
                    
                } else if (chosenShortcut == ConstantlyUsed.AddANewPieceShortcut)
                {
                    if (gameInstance.PlayerHasMovesLeft())
                    {
                        MakeANormalMoveWithoutAdditionalOptions(gameInstance);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Player does not have any more pieces left! Choose another option from the list! Press any key to try again!");
                        Console.ReadLine();
                    }
                    
                }
                else if (chosenShortcut == ConstantlyUsed.ExitShortcut)
                {
                    break;
                }
                if (gameInstance.SomebodyHasWon())
                {
                    ConsoleUI.Visualizer.AnnounceTheWinner(gameInstance);
                    break;
                }
                
                if (!gameInstance.ItsADraw()) continue;
                
                ConsoleUI.Visualizer.AnnounceTheDraw(gameInstance);
                _gameIsTerminated = true;
                break;
            
            } while (true);
        }
        
    }
    
    

    private void MakeANormalMoveWithoutAdditionalOptions(TicTacTwoBrain gameInstance)
    {
        
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        ConsoleUI.Visualizer.CommonMessageInEveryFirstRound(gameInstance);
        
        int boardWidth = ConstantlyUsed.CalculateMaxBoardWidthForCursor(gameInstance);
        int boardHeight = ConstantlyUsed.CalculateMaxBoardHeightForCursor(gameInstance);

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

            if (keyInfo.Key == ConsoleKey.S)
            {
                SaveCurrentGame(gameInstance);
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
        
        var moveWasSuccessful = gameInstance.MakeAMove(indexForX, indexForY);
        if (moveWasSuccessful)
        {
            gameInstance.ReducePieceCountForPlayer();
        }
    }

    private void MoveTheGrid(TicTacTwoBrain gameInstance)
    {
        
        int boardWidth = ConstantlyUsed.CalculateMaxBoardWidthForCursor(gameInstance);
        int boardHeight = ConstantlyUsed.CalculateMaxBoardHeightForCursor(gameInstance);
                
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

    private bool NewCenterSpotIsValid(int newGridMiddleSpotX, int newGridMiddleSpotY, TicTacTwoBrain gameInstance)
    {
        Console.Clear();

        var freeSpaceLeftToMove = (gameInstance.DimX - gameInstance.GetGrid().GetGridLength()) / 2;
        var currentGridMiddleSpotX = gameInstance.GetGrid().GetGridMiddleXValue();
        var currentGridMiddleSpotY = gameInstance.GetGrid().GetGridMiddleYValue();

        return Math.Abs(newGridMiddleSpotX - currentGridMiddleSpotX) <= freeSpaceLeftToMove &&
               Math.Abs(newGridMiddleSpotY - currentGridMiddleSpotY) <= freeSpaceLeftToMove;
    }

    private void MoveAPieceOnTheBoard(TicTacTwoBrain gameInstance)
    {
        ConsoleUI.Visualizer.DrawBoard(gameInstance);
        
        Console.WriteLine($"{gameInstance.GetNextOneToMove()} -> choose a piece to move: ");
                
        int boardWidth = ConstantlyUsed.CalculateMaxBoardWidthForCursor(gameInstance);
        int boardHeight = ConstantlyUsed.CalculateMaxBoardHeightForCursor(gameInstance);
                
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

    public string ChooseCurrentGameConfigurationMenu()
    {

        var configMenuItems = new Dictionary<string, MenuItem>();
        
        for (var i = 0; i < ConfigRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(returnValue, new MenuItem()
            {
                Title = ConfigRepository.GetConfigurationNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue,
                ShouldReturnByItself = true,
            });
        }

        var configMenu = new Menu(EMenuLevel.Deep, "TIC-TAC-TWO Choose config", configMenuItems);

        var shortcut = configMenu.Run();
        ChangeGameConfiguration(shortcut);

        return shortcut == ConstantlyUsed.ReturnToMainMenuShortcut ? ConstantlyUsed.ReturnToMainMenuShortcut : ConstantlyUsed.ReturnShortcut;
    }

    private void ChangeGameConfiguration(string shortcut)
    {
        
        if (shortcut == ConstantlyUsed.ExitShortcut ||  shortcut == ConstantlyUsed.ReturnToMainMenuShortcut || shortcut ==  ConstantlyUsed.ReturnShortcut)
        {
            return;
        }
        
        if (!int.TryParse(shortcut, out var chosenShortcutIndex))
        {
            return;
        }
        var chosenConfig = ConfigRepository.GetConfigurationByIndex(chosenShortcutIndex);

        _currentGameConfiguration = chosenConfig;
        
        ConsoleUI.Visualizer.AnnounceGameConfigChangeSuccess();
    }
    
    public string MakeNewGameConfigurationMenu()
    {
        
        Console.Clear();
        Console.WriteLine();
        var name = GetNewConfigName();
        var boardWidth = GetNewBoardWidth();
        var gridWidth = GetNewGridWidth(boardWidth);
        var winCondition = GetWinCondition();
        var howManyMovesTillAdvancedMoves = GetMovesNeededTillAdvancedMoves();
        var pieces = GetAmountOfPieces();
        
        
        var newGameConfiguration = new GameConfiguration()
        {
            Name = name,
            BoardDimension = boardWidth,
            GridDimension = gridWidth,
            WinCondition = winCondition,
            HowManyMovesTillAdvancedGameMoves = howManyMovesTillAdvancedMoves,
            PiecesPerPlayer = pieces,
            Grid = new Grid(boardWidth / 2, boardWidth / 2, boardWidth, gridWidth)

        };

        ConfigRepository.AddNewConfiguration(newGameConfiguration);
        
        ConsoleUI.Visualizer.AnnounceNewConfigAddedSuccess();
        return ConstantlyUsed.ReturnShortcut;
    }

    private int GetAmountOfPieces()
    {
        
        do
        {
            Console.Write("Please insert how many pieces each player has: ");
            var pieces = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pieces))
            {
                Console.WriteLine("You must insert something! ");
            }
            else if (!int.TryParse(pieces, out var piecesInInt))
            {
                Console.WriteLine("Please insert a valid number! You inserted unknown symbol! ");
            } else if (piecesInInt < 4)
            {
                Console.WriteLine("Please insert a number that is greater than 3 for the game to make sense! The original version has at least 4 pieces!");
            }
            else
            {
                return piecesInInt;
            }
            
        } while (true);
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


    private void SaveCurrentGame(TicTacTwoBrain gameInstance)
    {
        Console.Clear();
        Console.WriteLine("Please give a name for the game you want to save");
        var savedGameName = GetNameForTheGameSaving();
        GameRepository.SaveGame(gameInstance.GetGameStateJson(), savedGameName);
        Console.WriteLine("Game saved successfully! Press Enter to continue!");
        Console.ReadLine();
    }

    private string GetNameForTheGameSaving()
    {
        var nameIsInserted = false;
        var name = "";
        while (!nameIsInserted)
        {
            name = Console.ReadLine() ?? "";
            if (!String.IsNullOrWhiteSpace(name))
            {
                nameIsInserted = true;
            }
            
        }
        return name;
    }

}