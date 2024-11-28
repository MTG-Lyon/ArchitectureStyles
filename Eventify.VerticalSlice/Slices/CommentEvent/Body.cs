using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.Slices.CommentEvent;

public class Body
{
    [Required] public string Commenter { get; set; } = string.Empty;
    [Required] public string Comment { get; set; } = string.Empty;
}