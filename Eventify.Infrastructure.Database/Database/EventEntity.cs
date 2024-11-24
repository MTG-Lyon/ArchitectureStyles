using System.ComponentModel.DataAnnotations;

namespace Eventify.Infrastructure.Database.Database;

public class EventEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public virtual ICollection<ParticipantEntity> Participants { get; set; }
}