namespace Eventify.Hexagonal.Domain.Models;

public record EventName(string Value)
{
    public string Value { get; } = !string.IsNullOrWhiteSpace(Value) 
        ? Value 
        : throw new ArgumentException("The event name is required");
}