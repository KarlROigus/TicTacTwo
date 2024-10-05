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
                var currentSpot = gameInstance.GameBoard[x, y];
                var nextSpotXValue = x + 1 > gameInstance.DimX - 1 ? gameInstance.DimX - 1 : x + 1;
                var prevSpotXValue = x - 1 < 0 ? 0 : x - 1;
                var nextSpot = gameInstance.GameBoard[nextSpotXValue, y];
                var prevSpot = gameInstance.GameBoard[prevSpotXValue, y];
                
                if (IsPartOfGrid(currentSpot) && IsPartOfGrid(nextSpot))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                } else if (IsPartOfGrid(currentSpot) && IsPartOfGrid(prevSpot))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                
                Console.Write(" ");
                Console.Write(DrawGamePiece(gameInstance.GameBoard[x, y].GetSpotValue()));
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
                    SpotOnTheBoard currentSpot = gameInstance.GameBoard[x, y];
                    var nextSpotXValue = x + 1 > gameInstance.DimX - 1 ? gameInstance.DimX - 1 : x + 1;
                    var prevSpotXValue = x - 1 < 0 ? 0 : x - 1;
                    SpotOnTheBoard nextSpot = gameInstance.GameBoard[nextSpotXValue, y];
                    var prevSpot = gameInstance.GameBoard[prevSpotXValue, y];
                    var nextRowYValue = y + 1 > gameInstance.DimY - 1 ? gameInstance.DimY - 1 : y + 1;
                    SpotOnTheBoard nextRowSpot = gameInstance.GameBoard[x, nextRowYValue];
                    
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

}