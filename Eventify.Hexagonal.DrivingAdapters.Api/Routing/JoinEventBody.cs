using System.ComponentModel.DataAnnotations;

namespace Eventify.Hexagonal.Api.Routing;

public class JoinEventBody
{
    [Required] public string EmailAddress { get; set; } = default!;
}