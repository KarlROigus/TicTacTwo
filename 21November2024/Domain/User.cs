using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User
{
    public int UserId { get; set; }

    [MaxLength(128)]
    public string Username { get; set; } = default!;
    
    public ICollection<Config>? Configs { get; set; }
    public ICollection<Game>? Games { get; set; }
}