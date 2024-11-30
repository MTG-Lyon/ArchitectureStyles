using Eventify.Infrastructure.Database.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.VerticalSlice.Slices.CreateNewEvent;

public class ForCreatingEventRepository(EventifyDbContext dbContext) 
    : IForCheckingEventExists
{
    public async Task<bool> Exists(string name) =>
        await dbContext.Events.AnyAsync(x => x.Name == name);
}