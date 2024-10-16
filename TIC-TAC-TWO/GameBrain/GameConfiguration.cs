namespace GameBrain;

public record struct GameConfiguration()
{
    public string? Name { get; set; } = default!;
    
    public int BoardWidth { get; set; } = 5;
    public int BoardHeight { get; set; } = 5;

    public int GridHeight { get; set; } = 3;
    public int GridWidth { get; set; } = 3;


    public int WinCondition { get; set; } = 3;
    public int HowManyMovesTillAdvancedGameMoves { get; set; } = 2;

    public Grid Grid { get; set; } = new Grid(2, 2, 5, 3);
    
    
    public override string ToString()
    { 
        return
            $"Board {BoardWidth}x{BoardHeight}, to win: {WinCondition}, can move piece after {HowManyMovesTillAdvancedGameMoves}.";
    }
    
}