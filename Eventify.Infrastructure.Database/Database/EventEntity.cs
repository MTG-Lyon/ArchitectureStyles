using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.Infrastructure.Database;

public class EventEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
}