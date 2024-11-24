using System.ComponentModel.DataAnnotations;

namespace Domain;

public class State
{
    public int StateId { get; set; }

    [MaxLength(10240)]
    public string StateJson { get; set; } = default!;

    public int GameId { get; set; }
    public Game? Game { get; set; }
}