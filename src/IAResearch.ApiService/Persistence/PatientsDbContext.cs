using IAResearch.Domain;
using Microsoft.EntityFrameworkCore;
using Wolverine.EntityFrameworkCore;

namespace IAResearch.ApiService.Persistence;

public class PatientsDbContext : DbContext
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<HealthEvent> HealthEvents => Set<HealthEvent>();

    public PatientsDbContext(
        DbContextOptions<PatientsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.MapWolverineEnvelopeStorage();

        modelBuilder.ApplyConfiguration(new Configurations.HealthEventConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.PatientConfiguration());

    }
}
