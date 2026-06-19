using IAResearch.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAResearch.ApiService.Persistence.Configurations;

public class HealthEventConfiguration : IEntityTypeConfiguration<HealthEvent>
{
    public void Configure(EntityTypeBuilder<HealthEvent> entity)
    {
        entity.HasKey(h => h.Id);
        entity.Property(h => h.Description).HasMaxLength(2000).IsRequired();
        entity.Property(h => h.Diagnosis).HasMaxLength(2000).IsRequired();
        entity.Property(h => h.RecommendedTreatment).HasMaxLength(2000).IsRequired();

        entity.HasOne(h => h.Patient)
            .WithMany(p => p.HealthEvents)
            .HasForeignKey(h => h.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
