using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Config
{
    public int Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    
    [MaxLength(10240)]
    public string ConfigJsonString { get; set; } = default!;

    public ICollection<GameStateJson>? GameStateJsons { get; set; }
}