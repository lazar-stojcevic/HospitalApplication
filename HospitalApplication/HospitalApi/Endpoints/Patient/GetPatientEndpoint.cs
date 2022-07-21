using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Patient;

[HttpGet("patients/{id:guid}"), AllowAnonymous]
public class GetPatientEndpoint : Endpoint<GetPatientRequest, PatientResponse>
{
    private readonly IPatientService _patientService;

    public GetPatientEndpoint(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public override async Task HandleAsync(GetPatientRequest req, CancellationToken ct)
    {
        var patient = await _patientService.GetAsync(req.Id);

        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var patientResponse = patient.ToPatientResponse();
        await SendOkAsync(patientResponse, ct);
    }
}
