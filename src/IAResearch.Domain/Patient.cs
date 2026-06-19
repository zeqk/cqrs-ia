namespace IAResearch.Domain;

public class Patient : Entity
{
    public Guid Id { get; set; }

    public string PatientNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public ICollection<HealthEvent> HealthEvents { get; set; } = new List<HealthEvent>();
}
