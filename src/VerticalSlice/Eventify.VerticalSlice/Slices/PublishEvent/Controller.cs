using Eventify.VerticalSlice.Shared.Domain.Exceptions.Base;
using Eventify.VerticalSlice.Shared.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSlice.Slices.PublishEvent;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(UseCase useCase) : ControllerBase
{
    [HttpPut("{eventId}/publish")]
    public async Task<IActionResult> CreateNewEvent([FromRoute] Guid eventId)
    {
        try
        {
            await useCase.Publish(eventId);
            return Ok();
        }
        catch (Exception e) when(e is IDomainException)
        { 
            return Problem(statusCode: 403, title: e.Message);
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