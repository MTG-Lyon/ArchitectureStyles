using Eventify.Hexagonal.Application.Models.Exceptions.Base;

namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message), IDomainException;