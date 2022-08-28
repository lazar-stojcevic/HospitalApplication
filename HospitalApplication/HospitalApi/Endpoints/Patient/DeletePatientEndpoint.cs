using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Patient;

[HttpDelete("patients/{id:guid}"), Authorize(Roles = "ADMIN")]
public class DeletePatientEndpoint : Endpoint<DeletePatientRequest>
{
    private readonly IPatientService _patientService;

    public DeletePatientEndpoint(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public override async Task HandleAsync(DeletePatientRequest req, CancellationToken ct)
    {
        var deleted = await _patientService.DeleteAsync(req.Id);
        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
