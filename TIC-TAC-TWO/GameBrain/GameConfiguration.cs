namespace GameBrain;

public record struct GameConfiguration()
{
    public string Name { get; set; } = default!;
    
    public int BoardWidth { get; set; } = 5;
    public int BoardHeight { get; set; } = 5;

    public int GridHeight { get; set; } = 3;
    public int GridWidth { get; set; } = 3;

    public int GridMiddlePointXValue { get; set; } = 5 / 2;
    public int GridMiddlePointYValue { get; set; } = 5 / 2;

    public int WinCondition { get; set; } = 3;
    public int MovePieceAfterNMoves { get; set; } = 2;
    

    public override string ToString()
    { 
        return
            $"Board {BoardWidth}x{BoardHeight}, to win: {WinCondition}, can move piece after {MovePieceAfterNMoves}.";
    }
}