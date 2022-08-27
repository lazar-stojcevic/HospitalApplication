using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Patient;

[HttpGet("patients/{id:guid}"), Authorize]
public class GetPatientEndpoint : Endpoint<GetPatientRequest, PatientResponse>
{
    private readonly IPatientService _patientService;
    private readonly IAnonymizationService _anonymizationService;

    public GetPatientEndpoint(IPatientService patientService, IAnonymizationService anonymizationService)
    {
        _patientService = patientService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(GetPatientRequest req, CancellationToken ct)
    {
        var patient = await _patientService.GetAsync(req.Id);

        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var context = HttpContext;
        var result = string.Empty;
        if (context.User != null)
        {
            result = context.User.FindFirstValue(ClaimTypes.Role);
        }
        if (result.Equals("DOCTOR") || result.Equals("PATIENT"))
        {
            var patientResponse = patient.ToPatientResponse();
            await SendOkAsync(patientResponse, ct);
        }
        else if (result.Equals("ADMIN") || result.Equals("ACCOUNTANT"))
        {
            var anonymisedData = _anonymizationService.AnonymisePatiendData(patient);
            await SendOkAsync(anonymisedData, ct);
        }
        else
        {
            await SendUnauthorizedAsync(ct);
        }
    }
}
