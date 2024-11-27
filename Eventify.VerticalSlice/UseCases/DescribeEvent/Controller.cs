using Eventify.VerticalSlice.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSlice.UseCases.DescribeEvent;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(UseCase useCase) : ControllerBase
{
    [HttpPut("{eventId}/describe")]
    public async Task<IActionResult> CreateNewEvent([FromRoute] Guid eventId, [FromBody] Body body)
    {
        try
        {
            await useCase.Describe(eventId, body.Description);
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