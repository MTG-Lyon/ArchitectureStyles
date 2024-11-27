using System.ComponentModel.DataAnnotations;

namespace Eventify.Infrastructure.Database.Database;

public class CommentEntity
{
    [Key]
    public Guid EventId { get; set; }
    public EventEntity? Event { get; set; }
    
    public DateTime Date { get; set; }
    public string Commenter { get; set; } = default!;
    public string Comment { get; set; } = default!;
}