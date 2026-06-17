using IAResearch.ApiService.Domain;
using IAResearch.Infrastructure.DomainEvents;
using IAResearch.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace IAResearch.ApiService.Persistence;

public class PatientsDbContext : DbContext
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<HealthEvent> HealthEvents => Set<HealthEvent>();

    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public PatientsDbContext(
        DbContextOptions<PatientsDbContext> options, 
        IDomainEventsDispatcher domainEventsDispatcher) : base(options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.HealthEventConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.PatientConfiguration());

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        await _domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}
