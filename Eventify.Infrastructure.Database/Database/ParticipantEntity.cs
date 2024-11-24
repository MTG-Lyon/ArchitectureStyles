using System.ComponentModel.DataAnnotations;

namespace Eventify.Infrastructure.Database.Database;

public class ParticipantEntity
{
    [Key]
    public string EmailAddress { get; set; } = default!;
    
    public Guid EventId { get; set; }
    public EventEntity? Event { get; set; }
}