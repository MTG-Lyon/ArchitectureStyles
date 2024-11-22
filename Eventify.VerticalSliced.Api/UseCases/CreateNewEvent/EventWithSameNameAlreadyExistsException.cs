namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

internal class EventWithSameNameAlreadyExistsException(string message) : Exception(message);