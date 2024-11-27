using Eventify.VerticalSlice.Shared.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

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
        catch (Exception e) when(e is IDomainException)
        {
            return Problem(statusCode: 403, title: e.Message);
        }
        catch (ArgumentException e)
        {
            return Problem(statusCode: 403, title: e.Message);
        }
    }
}

