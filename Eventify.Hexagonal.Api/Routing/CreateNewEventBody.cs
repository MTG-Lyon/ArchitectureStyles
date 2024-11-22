using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.Api.Routing;

public class CreateNewEventBody
{
    [Required] public string Name { get; set; } = default!;
}