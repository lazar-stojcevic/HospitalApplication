using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Patient;

[HttpPut("patients/{id:guid}"), Authorize(Roles = "PATIENT")]
public class UpdatePatientEndpoint : Endpoint<UpdatePatientRequest, PatientResponse>
{
    private readonly IPatientService _patientService;

    public UpdatePatientEndpoint(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public override async Task HandleAsync(UpdatePatientRequest req, CancellationToken ct)
    {
        var context = HttpContext;
        var username = string.Empty;
        if (context.User != null)
        {
            username = context.User.FindFirstValue(ClaimTypes.Name);
        }

        var existingPatient = await _patientService.GetAsync(req.Id);

        if (existingPatient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!existingPatient.Username.Equals(username))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var patient = req.ToPatient();
        patient.SetPassword(existingPatient.Password);
        await _patientService.UpdateAsync(patient);

        var patinetResponse = patient.ToPatientResponse();
        await SendOkAsync(patinetResponse, ct);
    }
}
