using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.Slices.DescribeEvent;

public class Body
{
    [Required] public string Description { get; set; } = string.Empty;
}