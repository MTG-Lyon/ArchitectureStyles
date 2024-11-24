using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.Api.Routing;

public class RegisterEventBody
{
    [Required] public string Name { get; set; } = default!;
}

public class DescribeEventBody
{
    [Required] public string Description { get; set; } = default!;
}