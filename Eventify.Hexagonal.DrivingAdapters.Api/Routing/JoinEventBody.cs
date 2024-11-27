using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.DrivingAdapters.Api.Routing;

public class JoinEventBody
{
    [Required] public string EmailAddress { get; set; } = default!;
}