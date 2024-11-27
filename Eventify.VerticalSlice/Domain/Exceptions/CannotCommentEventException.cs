using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Domain.Exceptions;

public class CannotCommentEventException(string message) : Exception(message), IDomainException;