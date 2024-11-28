using Eventify.VerticalSlice.Shared.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSlice.Slices.DescribeEvent;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(IEventRepository eventRepository) : ControllerBase
{
    [HttpPut("{eventId}/describe")]
    public async Task<IActionResult> CreateNewEvent([FromRoute] Guid eventId, [FromBody] Body body)
    {
        try
        {
            var @event = await eventRepository.Get(eventId);

            @event.Describe(body.Description);

            await eventRepository.Save(@event);
            
            return Ok();
        }
        catch (EntityNotFoundException e)
        { 
            return Problem(statusCode: 404, title: e.Message);
        }
        catch (ArgumentException e)
        {
            return Problem(statusCode: 403, title: e.Message);
        }
    }
}