namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message);