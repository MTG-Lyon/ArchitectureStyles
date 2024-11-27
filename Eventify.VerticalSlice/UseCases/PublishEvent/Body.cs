using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.UseCases.PublishEvent;

public class Body
{
    [Required] public string Description { get; set; } = string.Empty;
}