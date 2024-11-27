using System.ComponentModel.DataAnnotations;

namespace Eventify.Clean.Presentation.Routing;

public class CreateNewEventBody
{
    [Required] public string Name { get; set; } = default!;
}