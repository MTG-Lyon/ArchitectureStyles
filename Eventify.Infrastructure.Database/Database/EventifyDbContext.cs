using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Database.Database;

public class EventifyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
}