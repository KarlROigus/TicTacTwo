using System.ComponentModel.DataAnnotations;

namespace Domain;

public class GameStateJson
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [MaxLength(10240)]
    public string GameStateJsonString { get; set; } = default!;

    public int ConfigId { get; set; }
    public Config? Config { get; set; }
}