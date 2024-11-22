using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSliced.Api.UseCases.ListExistingEvents;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(UseCase useCase) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> CreateNewEvent()
    {
        var results = await useCase.Execute();
        
        return Ok(results);
    }
}