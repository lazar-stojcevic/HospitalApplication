using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Mapping;
using HospitalApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints
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
