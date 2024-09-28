using GameBrain;

namespace ConsoleUI;

public static class  Visualizer
{
    public static void DrawBoard(TicTacTwoBrain gameInstance)
    {
        Console.WriteLine();
    
        for (var y = 0; y < gameInstance.DimY; y++)
        {
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
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
    
    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.X => "X",
            EGamePiece.O => "O",
            _ => " "
        };
}