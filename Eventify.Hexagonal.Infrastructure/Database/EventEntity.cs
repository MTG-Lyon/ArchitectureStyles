using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.Infrastructure.Database;

public class EventEntity
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
}