using FastEndpoints;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Patient
{
    [HttpGet("patients"), AllowAnonymous]
    public class GetAllPatientsEndpoint : Endpoint<EmptyRequest, GetAllPatientsResponse>
    {
        private readonly IPatientService _patientService;

        public GetAllPatientsEndpoint(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            var patients = await _patientService.GetAllAsync();

            if (patients is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var patientResponse = patients.ToPatientsResponse();
            await SendOkAsync(patientResponse, ct);
        }
    }
}
