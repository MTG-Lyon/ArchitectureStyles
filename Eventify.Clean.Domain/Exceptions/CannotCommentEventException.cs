using Eventify.Clean.Domain.Exceptions.Base;

namespace Eventify.Clean.Domain.Exceptions;

public class CannotCommentEventException(string message) : Exception(message), IDomainException;