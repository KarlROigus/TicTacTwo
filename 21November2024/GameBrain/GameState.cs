namespace GameBrain;

public class GameState
{
    public Grid Grid { get; set; }
    public SpotOnTheBoard[][] GameBoard { get; set; }
    public EGamePiece NextMoveBy { get; set; }

    public GameConfiguration GameConfiguration { get; set; }

    public int MovesMade { get; set; }

    public int PiecesForPlayerX { get; set; }

    public int PiecesForPlayerO { get; set; }

    public string CurrentOneToMove { get; set; }

    public string LoginUser { get; set; }

    public GameState(Grid grid, SpotOnTheBoard[][] gameBoard, GameConfiguration gameConfiguration,
        int movesMade, EGamePiece nextMoveBy, int piecesForPlayerX, int piecesForPlayerO, string currentOneToMove,
        string loginUser)
    
    {
        Grid = grid;
        GameBoard = gameBoard;
        GameConfiguration = gameConfiguration;
        MovesMade = movesMade;
        NextMoveBy = nextMoveBy;
        PiecesForPlayerX = piecesForPlayerX;
        PiecesForPlayerO = piecesForPlayerO;
        CurrentOneToMove = currentOneToMove;
        LoginUser = loginUser;
    }
    
    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
}