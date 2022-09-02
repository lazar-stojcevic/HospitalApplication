using FastEndpoints;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Patient
{
    [HttpGet("patients"), Authorize(Roles = "ADMIN,DOCTOR,ACCOUNTANT")]
    public class GetAllPatientsEndpoint : Endpoint<EmptyRequest, GetAllPatientsResponse>
    {
        private readonly IPatientService _patientService;
        private readonly IAnonymizationService _anonymizationService;

        public GetAllPatientsEndpoint(IPatientService patientService, IAnonymizationService anonymizationService)
        {
            _patientService = patientService;
            _anonymizationService = anonymizationService;
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            var patients = await _patientService.GetAllAsync();

            if (patients is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var context = HttpContext;
            var result = string.Empty;
            if (context.User != null)
            {
                result = context.User.FindFirstValue(ClaimTypes.Name);
            }

            var patientResponse = patients.ToPatientsResponse();
            await SendOkAsync(_anonymizationService.AnonymiseAllPatientsExceptCurrent(patientResponse, result), ct);
        }
    }
}
