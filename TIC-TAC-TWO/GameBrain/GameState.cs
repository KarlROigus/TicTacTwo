namespace GameBrain;

public class GameState
{
    public Grid Grid;
    public SpotOnTheBoard[][] GameBoard;
    public EGamePiece NextMoveBy { get; set; } = EGamePiece.X;

    public readonly GameConfiguration GameConfiguration;

    public int MovesMade;

    public GameState(Grid grid, GameConfiguration gameConfiguration, int movesMade)
    {
        Grid = grid;
        GameConfiguration = gameConfiguration;
        MovesMade = movesMade;
        GameBoard = MakeGameBoard(gameConfiguration);

    }
    
    private SpotOnTheBoard[][] MakeGameBoard(GameConfiguration gameConfiguration)
    {
        var gameBoard = new SpotOnTheBoard[gameConfiguration.BoardHeight][];
        for (int y = 0; y < gameConfiguration.BoardHeight; y++)
        {
            gameBoard[y] = new SpotOnTheBoard[gameConfiguration.BoardWidth];

            for (int x = 0; x < gameConfiguration.BoardWidth; x++)
            {
                gameBoard[y][x] = new SpotOnTheBoard(EGamePiece.Empty, CheckIfSpotIsPartOfGrid(x, y));
            }
        }

        return gameBoard;
    }
    
    private bool CheckIfSpotIsPartOfGrid(int x, int y)
    {
        return Grid.BooleanAt(x, y);
    }
}