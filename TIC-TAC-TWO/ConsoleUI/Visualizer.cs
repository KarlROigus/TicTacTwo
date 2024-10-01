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
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y].GetSpotValue()) + " ");
                if (x != gameInstance.DimX - 1)
                {
                    Console.Write("|");
                }
            }

            if (y != gameInstance.DimY - 1)
            {
                
                Console.WriteLine();
                for (var x = 0; x < gameInstance.DimX; x++)
                {
                    Console.Write("---");
                    if (x != gameInstance.DimX - 1)
                    {
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
    
    public static void DrawGrid(TicTacTwoBrain gameInstance)
    {
        
        Console.ForegroundColor = ConsoleColor.DarkRed;
        var middlePointX = ((gameInstance.DimX * 4) / 2) - 1;
        var middlePointY = ((gameInstance.DimY * 2) - 1) / 2;
        
        var left = middlePointX - 5;
        var top = gameInstance.DimY - 3;
        
        for (var y = 0; y < 3; y++)
        {
            Console.SetCursorPosition(left, top);
            for (var x = 0; x < 3; x++)
            {
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y].GetSpotValue()) + " ");
                if (x != 2)
                {
                    Console.Write("|");
                }
            }

            Console.WriteLine();
            if (y != 2)
            {
                Console.SetCursorPosition(left, ++top);
                for (var x = 0; x < 3; x++)
                {
                    Console.Write("---");
                    if (x != 2)
                    {
                        Console.Write("+");
                    }
                }
            }

            Console.SetCursorPosition(left, ++top);
            Console.WriteLine();
            
        }
        
        Console.SetCursorPosition(middlePointX, middlePointY);
        Console.Write("M");
        
        
        Console.SetCursorPosition(0, gameInstance.DimX * 2);
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine(((gameInstance.DimY * 2) - 1) / 2);
        Console.WriteLine(middlePointX);
        Console.WriteLine(middlePointY);
    }
    
    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.X => "X",
            EGamePiece.O => "O",
            _ => " "
        };

    private static string DrawGamePiecee(SpotOnTheBoard spot)
    {

        
        return spot.GetSpotValue() switch
        {
            EGamePiece.O => "O",
            EGamePiece.X => "X",
            _ => " "
        };
    }

}