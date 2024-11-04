namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = "Default TIC-TAC-TWO";
    public int BoardDimension { get; set; } = 5;
    public int GridDimension { get; set; } = 3;
    public int WinCondition { get; set; } = 3;
    public int HowManyMovesTillAdvancedGameMoves { get; set; } = 2;
    public int PiecesPerPlayer { get; set; } = 4;
    public Grid Grid { get; set; } = new Grid(2, 2, 5, 3);
    public override string ToString()
    { 
        return
            $"Board {BoardDimension}x{BoardDimension}, to win: {WinCondition}, can move piece after {HowManyMovesTillAdvancedGameMoves}.";
    }
    
    public SpotOnTheBoard[][] GetFreshGameBoard(GameConfiguration currentConfig, Grid grid)
    {
        var gameBoard = new SpotOnTheBoard[currentConfig.BoardDimension][];
        for (int y = 0; y < currentConfig.BoardDimension; y++)
        {
            gameBoard[y] = new SpotOnTheBoard[currentConfig.BoardDimension];

            for (int x = 0; x < currentConfig.BoardDimension; x++)
            {
                gameBoard[y][x] = new SpotOnTheBoard(EGamePiece.Empty, CheckIfSpotIsPartOfGrid(x, y, grid));
            }
        }

        return gameBoard;
    }
    
    private bool CheckIfSpotIsPartOfGrid(int x, int y, Grid grid)
    {
        return grid.BooleanAt(x, y);
    }
    
    
    
}