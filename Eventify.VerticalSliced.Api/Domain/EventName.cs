namespace Eventify.VerticalSliced.Api.Domain;

public record EventName(string Value)
{
    public string Value { get; } = !string.IsNullOrWhiteSpace(Value) 
        ? Value 
        : throw new ArgumentNullException("The event name is required");
}