namespace Eventify.Clean.Domain;

public record EmailAddress(string Value)
{
    public string Value { get; } = !string.IsNullOrWhiteSpace(Value) 
        ? Value 
        : throw new ArgumentException("The email address is required");
}