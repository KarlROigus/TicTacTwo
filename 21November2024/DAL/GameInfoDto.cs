namespace DAL;

public class GameInfoDto
{
    public string GameName { get; set; } = default!;

    public string PlayerX { get; set; } = default!;

    public string? PlayerO { get; set; }

    public string GameStateJson { get; set; } = default!;
}