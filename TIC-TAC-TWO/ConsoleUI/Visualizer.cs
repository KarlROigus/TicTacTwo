using GameBrain;

namespace ConsoleUI;

public static class  Visualizer
{
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        
        Console.Clear();
        
        for (int y = 0; y < gameInstance.DimY; y++)
        {
            for (int x = 0; x < gameInstance.DimX; x++)
            {
                var currentSpot = gameInstance.GameBoard[y][x];
                var nextSpotXValue = x + 1 > gameInstance.DimX - 1 ? gameInstance.DimX - 1 : x + 1;
                var prevSpotXValue = x - 1 < 0 ? 0 : x - 1;
                var nextSpot = gameInstance.GameBoard[y][nextSpotXValue];
                var prevSpot = gameInstance.GameBoard[y][prevSpotXValue];
                
                if (IsPartOfGrid(currentSpot) && IsPartOfGrid(nextSpot))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                } else if (IsPartOfGrid(currentSpot) && IsPartOfGrid(prevSpot))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                
                Console.Write(" ");
                Console.Write(DrawGamePiece(gameInstance.GameBoard[y][x].GetSpotValue()));
                Console.Write(" ");
                if (x < gameInstance.DimX - 1)
                {
                    if (!IsPartOfGrid(nextSpot))
                    {
                        Console.ResetColor();
                    }
                    Console.Write("|");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
            if (y < gameInstance.DimY - 1)
            {
                for (int x = 0; x < gameInstance.DimX; x++)
                {
                    SpotOnTheBoard currentSpot = gameInstance.GameBoard[y][x];
                    var nextSpotXValue = x + 1 > gameInstance.DimX - 1 ? gameInstance.DimX - 1 : x + 1;
                    var prevSpotXValue = x - 1 < 0 ? 0 : x - 1;
                    SpotOnTheBoard nextSpot = gameInstance.GameBoard[y][nextSpotXValue];
                    var prevSpot = gameInstance.GameBoard[y][prevSpotXValue];
                    var nextRowYValue = y + 1 > gameInstance.DimY - 1 ? gameInstance.DimY - 1 : y + 1;
                    SpotOnTheBoard nextRowSpot = gameInstance.GameBoard[nextRowYValue][x];
                    
                    if (IsPartOfGrid(currentSpot) && IsPartOfGrid(nextSpot) && IsPartOfGrid(nextRowSpot)) 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    
                    Console.Write("---");
                    if (x < gameInstance.DimX - 1)
                    {
                        if (!IsPartOfGrid(nextSpot))
                        {
                            Console.ResetColor();
                        }
                        Console.Write("+");
                    }
                }
                Console.WriteLine();
                
            }
            
        }

        Console.WriteLine();
    }

    private static bool IsPartOfGrid(SpotOnTheBoard currentSpot)
    {
        return currentSpot.IsPartOfGrid;
    }
    
    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.X => "X",
            EGamePiece.O => "O",
            _ => " "
        };

    public static void AnnounceGameConfigChangeSuccess()
    {
        Console.WriteLine();
        Console.WriteLine("Game configuration changed succesfully! Press Enter to continue.");
        Console.ReadLine();
        Console.WriteLine();
    }

    public static void AnnounceNewConfigAddedSuccess()
    {
        Console.WriteLine();
        Console.WriteLine("New configuration added successfully! Now go on and choose your configuration!" +
                          "Press Enter to continue.");
        Console.ReadLine();
        Console.WriteLine();
    }

    public static void CommonMessageInEveryFirstRound(TicTacTwoBrain gameInstance)
    {
        Console.WriteLine("Making a move - use arrows keys to move around, press Enter to select a location.");
        Console.WriteLine("Current one to move: " + gameInstance.GetNextOneToMove());
        Console.WriteLine();
        Console.WriteLine("Press S to SAVE GAME.");
        Console.WriteLine("Press Q to QUIT.");
    }

    public static void AnnounceTheWinner(TicTacTwoBrain gameInstance)
    {
        Console.Clear();
        DrawBoard(gameInstance);
        Console.WriteLine();
        Console.WriteLine($"{gameInstance.GetPreviousMover()} has won the game!");
        Console.WriteLine("Press any key to return to the main menu!");
        Console.ReadLine();
        Console.Clear();
    }

    public static void AnnounceTheDraw(TicTacTwoBrain gameInstance)
    {
        Console.Clear();
        DrawBoard(gameInstance);
        Console.WriteLine();
        Console.WriteLine($"Game has ended in a draw!");
        Console.WriteLine("Press any key to return to the main menu!");
        Console.ReadLine();
        Console.Clear();
    }

}