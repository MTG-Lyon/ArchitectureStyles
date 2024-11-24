namespace Eventify.Hexagonal.Infrastructure.Adapters;

public class EntityNotFoundException(string message) : Exception(message);