using System.ComponentModel.DataAnnotations;

namespace Eventify.Clean.Presentation.Routing.Bodies;

public class RegisterEventBody
{
    [Required] public string Name { get; set; } = default!;
}

public class DescribeEventBody
{
    [Required] public string Description { get; set; } = default!;
}