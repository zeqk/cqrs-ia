using IAResearch.ApiService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAResearch.ApiService.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> entity)
    {
        entity.HasKey(p => p.Id);
        entity.Property(p => p.PatientNumber).HasMaxLength(10).IsRequired();
        entity.Property(p => p.FirstName).HasMaxLength(120).IsRequired();
        entity.Property(p => p.LastName).HasMaxLength(120).IsRequired();
    }
}
