using Microsoft.EntityFrameworkCore;

namespace Eventify.Hexagonal.Infrastructure.Database;

public class EventifyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
}