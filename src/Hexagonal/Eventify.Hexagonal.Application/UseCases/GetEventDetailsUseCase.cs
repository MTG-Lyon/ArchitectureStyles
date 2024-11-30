using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;

namespace Eventify.Hexagonal.Application.UseCases;

internal class GetEventDetailsUseCase(IEventRepository eventRepository) 
    : IGetEventDetailsUseCase
{
    public Task<EventDetailsDto> GetEventDetails(Guid eventId) =>
        eventRepository.GetDetails(eventId);
}