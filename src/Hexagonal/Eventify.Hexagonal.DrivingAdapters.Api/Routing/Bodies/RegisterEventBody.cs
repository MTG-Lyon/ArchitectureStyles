using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.DrivingAdapters.Api.Routing.Bodies;

public class RegisterEventBody
{
    [Required] public string Name { get; set; } = default!;
}

public class DescribeEventBody
{
    [Required] public string Description { get; set; } = default!;
}