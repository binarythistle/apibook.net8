using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string PlatformName { get; set; }
}