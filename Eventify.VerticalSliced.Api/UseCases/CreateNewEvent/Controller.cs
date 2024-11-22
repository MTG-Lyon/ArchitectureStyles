using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(UseCase useCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateNewEvent([FromBody] Body body)
    {
        try
        {
            await useCase.CreateNewEvent(body.Name);
            return Ok();
        }
        catch (EventWithSameNameAlreadyExistsException e)
        {
            return Problem(statusCode: 403, title: e.Message);
        }
        catch (ArgumentException e)
        {
            return Problem(statusCode: 403, title: e.Message);
        }
    }
}