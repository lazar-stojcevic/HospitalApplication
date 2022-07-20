using FastEndpoints;
using HospitalApi.Contracts.Requests;
using HospitalApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints;

[HttpDelete("patients/{id:guid}"), AllowAnonymous]
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
