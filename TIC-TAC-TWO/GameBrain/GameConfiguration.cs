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
    
}