namespace GameBrain;

public class GameState
{
    public Grid Grid { get; set; }
    public SpotOnTheBoard[][] GameBoard { get; set; }
    public EGamePiece NextMoveBy { get; set; } = EGamePiece.X;

    public GameConfiguration GameConfiguration { get; set; }

    public int MovesMade { get; set; }

    public GameState(Grid grid, SpotOnTheBoard[][] gameBoard, GameConfiguration gameConfiguration, int movesMade, EGamePiece nextMoveBy)
    {
        Grid = grid;
        GameBoard = gameBoard;
        GameConfiguration = gameConfiguration;
        MovesMade = movesMade;
        NextMoveBy = nextMoveBy;
    }
    
    

    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
}