using Eventify.Infrastructure.Database.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.VerticalSlice.UseCases.ListExistingEvents;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(EventifyDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> CreateNewEvent()
    {
        var results = await dbContext.Events
            .Select(x => new EventListItemDto(
                x.Id,
                x.Name,
                x.Description ?? string.Empty,
                x.Status,
                x.Participants
                    .Select(y => new ParticipantDto(y.EmailAddress))
                    .ToList()
            ))
            .ToListAsync();
        
        return Ok(results);
    }
}
    