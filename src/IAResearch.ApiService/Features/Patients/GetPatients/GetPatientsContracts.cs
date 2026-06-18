namespace IAResearch.ApiService.Features.Patients.GetPatients;

public sealed record GetPatientsQuery;

public sealed record GetPatientsResponse(IReadOnlyList<GetPatientsItem> Patients);

public sealed record GetPatientsItem(
    Guid Id,
    string Number,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth);
