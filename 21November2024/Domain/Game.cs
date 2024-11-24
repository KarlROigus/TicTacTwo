using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Game
{

    public int GameId { get; set; }
    
    [MaxLength(128)] public string GameName { get; set; } = default!;
    
    
    public int PlayerXUserId { get; set; }
    public User? PlayerX { get; set; }

    
    public int? PlayerOUserId { get; set; }
    public User? PlayerO { get; set; }
    
    public ICollection<State>? States { get; set; }
}