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

    public static string ConfigExtension = ".config.json";
    public static string GameExtension = ".game.json";

}