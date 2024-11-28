using System.ComponentModel.DataAnnotations;

namespace Eventify.Clean.Presentation.Routing.Bodies;

public class CommentEventBody
{
    [Required] public string Commenter { get; set; } = string.Empty;
    [Required] public string Comment { get; set; } = string.Empty;
}