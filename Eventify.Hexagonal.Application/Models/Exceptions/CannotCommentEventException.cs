using Eventify.Hexagonal.Application.Models.Exceptions.Base;

namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class CannotCommentEventException(string message) : Exception(message), IDomainException;