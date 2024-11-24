namespace Eventify.Hexagonal.Domain.Models;

public class EventAlreadyPublishedException(string message) : Exception(message);