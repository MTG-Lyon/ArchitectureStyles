using Eventify.VerticalSlice.Shared.Domain.Exceptions.Base;
using Eventify.VerticalSlice.Shared.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSlice.Slices.JoinEvent;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(UseCase useCase) : ControllerBase
{
    [HttpPost("{eventId}/participants")]
    public async Task<IActionResult> JoinEvent([FromRoute] Guid eventId, [FromBody] Body body)
    {
        try
        {
            await useCase.JoinEvent(eventId, body.EmailAddress);
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