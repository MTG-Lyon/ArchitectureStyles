using System.ComponentModel.DataAnnotations;

namespace Eventify.Clean.Presentation.Routing.Bodies;

public class JoinEventBody
{
    [Required] public string EmailAddress { get; set; } = default!;
}