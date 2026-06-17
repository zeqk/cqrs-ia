using IAResearch.ApiService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAResearch.ApiService.Features.Patients.GetPatients;

public static class GetPatientsHandler
{
    public static async Task<GetPatientsResponse> Handle(
        GetPatientsQuery query,
        PatientsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        List<GetPatientsItem> patients = await dbContext.Patients
            .AsNoTracking()
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ThenBy(p => p.Id)
            .Select(p => new GetPatientsItem(
                p.Id,
                p.FirstName,
                p.LastName,
                p.DateOfBirth))
            .ToListAsync(cancellationToken);

        return new GetPatientsResponse(patients);
    }
}
