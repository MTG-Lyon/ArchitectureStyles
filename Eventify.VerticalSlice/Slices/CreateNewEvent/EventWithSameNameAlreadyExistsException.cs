using Eventify.VerticalSlice.Shared.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Slices.CreateNewEvent;

internal class EventWithSameNameAlreadyExistsException(string message) : Exception(message), IDomainException;