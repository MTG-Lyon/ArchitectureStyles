using Eventify.Infrastructure.Database.Database;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.VerticalSlice.Slices.DescribeEvent;

[ApiController]
[Route("events")]
[Produces("application/json")]
public class Controller(EventifyDbContext dbContext) : ControllerBase
{
    [HttpPut("{eventId}/describe")]
    public async Task<IActionResult> CreateNewEvent([FromRoute] Guid eventId, [FromBody] Body body)
    {
        var @event = await dbContext.Events.FindAsync(eventId);
        
        if(@event is null)
        {
            return NotFound();
        }

        @event.Description = body.Description;
        
        await dbContext.SaveChangesAsync();
        
        return Ok();
    }
}