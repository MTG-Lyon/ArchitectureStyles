using Eventify.Hexagonal.Application.Models.Exceptions.Base;

namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class EventNotPublishedYetException(string message) : Exception(message), IDomainException;