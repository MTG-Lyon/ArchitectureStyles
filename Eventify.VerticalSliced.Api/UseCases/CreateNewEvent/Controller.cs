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
            await useCase.Execute(body.Name);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}