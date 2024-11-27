using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.UseCases.DescribeEvent;

public class Body
{
    [Required] public string Description { get; set; } = string.Empty;
}