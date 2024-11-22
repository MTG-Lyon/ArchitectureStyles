using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

public class Body
{
    [Required] public string Name { get; set; } = default!;
}