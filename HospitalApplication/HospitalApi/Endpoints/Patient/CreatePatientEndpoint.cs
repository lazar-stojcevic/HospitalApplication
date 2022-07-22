using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Patient;

[HttpPost("patients"), AllowAnonymous]
public class CreatePatientEndpoint : Endpoint<CreatePatientRequest, PatientResponse>
{
    private readonly IPatientService _patientService;
    private readonly IAccountService _accountService;

    public CreatePatientEndpoint(IPatientService patientService, IAccountService accountService)
    {
        _patientService = patientService;
        _accountService = accountService;
    }

    public override async Task HandleAsync(CreatePatientRequest req, CancellationToken ct)
    {
        var patient = req.ToPatient();

        await _patientService.CreateAsync(patient);

        await _accountService.CreateAccount(patient.Id.Value);

        var patientResponse = patient.ToPatientResponse();
        await SendCreatedAtAsync<GetPatientEndpoint>(
            new { Id = patient.Id.Value }, patientResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}
