using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Game
{
    public int GameId { get; set; }

    [MaxLength(128)] public string GameName { get; set; } = default!;

    public ICollection<GameState> GameStates { get; set; }
    
    // First User relationship
    public int PrimaryUserId { get; set; }
    public User? PrimaryUser { get; set; }

    // Second User relationship
    public int SecondaryUserId { get; set; }
    public User? SecondaryUser { get; set; }
}