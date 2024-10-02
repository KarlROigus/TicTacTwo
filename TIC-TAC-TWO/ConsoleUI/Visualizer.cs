using GameBrain;

namespace ConsoleUI;

public static class  Visualizer
{
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        Console.Clear();
    
        for (var y = 0; y < gameInstance.DimY; y++)
        {
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                SpotOnTheBoard currentSpot = gameInstance.GameBoard[x, y];
                

                var nextValueForX = x + 1 > gameInstance.DimX - 1 ? gameInstance.DimX - 1 : x + 1;

                SpotOnTheBoard nextSpot = gameInstance.GameBoard[nextValueForX, y];

                if (IsPartOfGrid(currentSpot) && IsPartOfGrid(nextSpot))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y].GetSpotValue()) + " ");

                if (x == gameInstance.DimX - 2)
                {
                    Console.ResetColor();
                }
                
                if (x != gameInstance.DimX - 1)
                {
                    Console.Write("|");
                }
                Console.ResetColor();
            }

            if (y != gameInstance.DimY - 1)
            {
                
                
                Console.WriteLine();
                for (var x = 0; x < gameInstance.DimX; x++)
                {
                    SpotOnTheBoard currentSpot = gameInstance.GameBoard[x, y];

                    var spotBelowYValue = y + 1 > gameInstance.DimY - 1 ? gameInstance.DimY - 1 : y + 1;
                    
                    SpotOnTheBoard spotBelow = gameInstance.GameBoard[x, spotBelowYValue];

                    if (IsPartOfGrid(currentSpot) && IsPartOfGrid(spotBelow))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write("---");

                    if (x == gameInstance.DimX - 2)
                    {
                        Console.ResetColor();
                    }
                    
                    if (x != gameInstance.DimX - 1)
                    {
                        var nextValueForX = x + 1 > gameInstance.DimX - 1 ? gameInstance.DimX - 1 : x + 1;
                        SpotOnTheBoard nextSpot = gameInstance.GameBoard[nextValueForX, y];
                        if (!IsPartOfGrid(nextSpot))
                        {
                            Console.ResetColor();
                        }
                        Console.Write("+");
                    }
                }
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine();
        }
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

}