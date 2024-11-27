namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class EventAlreadyPublishedException(string message) : Exception(message);