using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Patient;

[HttpPut("patients/block/{id:guid}"), Authorize(Roles = "ADMIN")]
public class BlockPatientEndpoint : Endpoint<BlockPatientRequest, bool>
{
    private readonly IPatientService _patientService;
    public BlockPatientEndpoint(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public override async Task HandleAsync(BlockPatientRequest req, CancellationToken ct)
    {
        var patient = await _patientService.GetAsync(req.Id, true);

        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        patient.IsActive = false;
        patient.SetPassword(patient.Password);
        patient.SetAccountId(patient.AccountId.Value);
        await _patientService.UpdateAsync(patient);

        await SendOkAsync(true, ct);
    }
}

