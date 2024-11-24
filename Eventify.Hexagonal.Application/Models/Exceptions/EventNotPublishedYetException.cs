namespace Eventify.Hexagonal.Domain.Models;

public class EventNotPublishedYetException(string message) : Exception(message);