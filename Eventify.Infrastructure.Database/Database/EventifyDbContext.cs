using Microsoft.EntityFrameworkCore;

namespace Eventify.Infrastructure.Database.Database;

public class EventifyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<EventEntity>()
            .HasMany<ParticipantEntity>(x => x.Participants)
            .WithOne(x => x.Event);
    }
}