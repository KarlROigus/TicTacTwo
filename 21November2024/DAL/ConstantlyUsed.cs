using GameBrain;

namespace DAL;

public static class ConstantlyUsed
{
    public static readonly string AddANewPieceShortcut = "A";
    public static readonly string MoveAPieceOnTheBoardShortcut = "B";
    public static readonly string ChangeGridPositionShortcut = "C";

    public static readonly string ExitShortcut = "E";
    public static readonly string ReturnToMainMenuShortcut = "M";
    public static readonly string ReturnShortcut = "R";
    
    public static readonly string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                             + Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;

    public static readonly string UserFolderPath = BasePath + "Users" + Path.DirectorySeparatorChar;

    public static readonly string ConfigExtension = ".config.json";
    public static readonly string GameExtension = ".game.json";

    public static readonly int ClassicalGame = -1;

    public static int CalculateMaxBoardWidthForCursor(TicTacTwoBrain gameInstance)
    {
        return (gameInstance.DimX - 1) * 4 + 1;
    }
    
    public static int CalculateMaxBoardHeightForCursor(TicTacTwoBrain gameInstance)
    {
        return (gameInstance.DimY - 1) * 2;
    }
    

}