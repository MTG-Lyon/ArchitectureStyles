using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.Slices.CreateNewEvent;

public class Body
{
    [Required] public string Name { get; set; } = default!;
}