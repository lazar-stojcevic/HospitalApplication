using FastEndpoints;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Patient;

[HttpPut("patients/{id:guid}"), AllowAnonymous]
public class UpdatePatientEndpoint : Endpoint<UpdatePatientRequest, PatientResponse>
{
    private readonly IPatientService _patientService;

    public UpdatePatientEndpoint(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public override async Task HandleAsync(UpdatePatientRequest req, CancellationToken ct)
    {
        var existingPatient = await _patientService.GetAsync(req.Id);

        if (existingPatient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var patient = req.ToPatient();
        patient.SetPassword(existingPatient.Password);
        await _patientService.UpdateAsync(patient);

        var patinetResponse = patient.ToPatientResponse();
        await SendOkAsync(patinetResponse, ct);
    }
}
