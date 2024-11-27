using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

public class Body
{
    [Required] public string Name { get; set; } = default!;
}