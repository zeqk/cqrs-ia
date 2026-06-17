using IAResearch.Infrastructure.Entities;

namespace IAResearch.ApiService.Domain;

public class HealthEvent : Entity
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string RecommendedTreatment { get; set; } = string.Empty;

    public Patient? Patient { get; set; }
}
