using FastEndpoints;
using HospitalApi.Contracts.Requests;
using HospitalApi.Contracts.Responses;
using HospitalApi.Mapping;
using HospitalApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints;

[HttpPost("patients"), AllowAnonymous]
public class CreatePatientEndpoint : Endpoint<CreatePatientRequest, PatientResponse>
{
    private readonly IPatientService _patientService;

    public CreatePatientEndpoint(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public override async Task HandleAsync(CreatePatientRequest req, CancellationToken ct)
    {
        var patient = req.ToPatient();

        await _patientService.CreateAsync(patient);

        var patientResponse = patient.ToPatientResponse();
        await SendCreatedAtAsync<GetPatientEndpoint>(
            new { Id = patient.Id.Value }, patientResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}
