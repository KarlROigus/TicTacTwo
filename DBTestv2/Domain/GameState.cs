using System.ComponentModel.DataAnnotations;

namespace Domain;

public class GameState
{
    public int GameStateId { get; set; }

    [MaxLength(10240)]
    public string GameStateJson { get; set; } = default!;

    public int GameId { get; set; }
    public Game? Game { get; set; }
}