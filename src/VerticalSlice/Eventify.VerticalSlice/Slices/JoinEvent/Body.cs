using System.ComponentModel.DataAnnotations;

namespace Eventify.VerticalSlice.Slices.JoinEvent;

public class Body
{
    [Required] public string EmailAddress { get; set; } = string.Empty;
}