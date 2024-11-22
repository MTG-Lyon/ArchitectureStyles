using System.ComponentModel.DataAnnotations;

namespace Eventify.Clean.Api;

public class CreateNewEventBody
{
    [Required] public string Name { get; set; } = default!;
}