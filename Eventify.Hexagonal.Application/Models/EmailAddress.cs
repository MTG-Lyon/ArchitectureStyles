namespace Eventify.Hexagonal.Domain.Models;

public record EmailAddress(string Value)
{
    public string Value { get; } = !string.IsNullOrWhiteSpace(Value) 
        ? Value 
        : throw new ArgumentException("The email address is required");
}