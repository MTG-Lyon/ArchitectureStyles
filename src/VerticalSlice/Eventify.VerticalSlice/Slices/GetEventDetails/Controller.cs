using Eventify.Infrastructure.Database.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.VerticalSlice.Slices.GetEventDetails;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(EventifyDbContext dbContext) : ControllerBase
{
    [HttpGet("{eventId}")]
    public async Task<IActionResult> CreateNewEvent([FromRoute] Guid eventId)
    {
        var results = await dbContext.Events
            .Where(x => x.Id == eventId)
            .Select(x => new EventDetailsDto(
                x.Comments
                    .Select(c => new CommentDto(c.Date, c.Commenter, c.Comment))
                    .OrderBy(c => c.Date)
                    .ToList()
            ))
            .FirstOrDefaultAsync();
        
        return Ok(results);
    }
}