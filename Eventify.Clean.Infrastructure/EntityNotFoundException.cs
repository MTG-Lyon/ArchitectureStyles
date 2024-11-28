namespace Eventify.Clean.Infrastructure;

public class EntityNotFoundException(string message) : Exception(message);