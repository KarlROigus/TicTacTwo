using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Config
{
    public int ConfigId { get; set; }

    [MaxLength(128)]
    public string ConfigName { get; set; } = default!;
    
    [MaxLength(10240)]
    public string ConfigJsonString { get; set; } = default!;

    public int UserId {
        get;
        set;
    }

    public User? User { get; set; }
}