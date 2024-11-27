using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

internal class EventWithSameNameAlreadyExistsException(string message) : Exception(message), IDomainException;