using FastEndpoints;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Doctor;

[HttpGet("doctors"), Authorize]
public class GetAllDoctorsEndpoint : Endpoint<EmptyRequest, GetAllDoctorsResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IAnonymizationService _anonymizationService;

    public GetAllDoctorsEndpoint(IDoctorService doctorService, IAnonymizationService anonymizationService)
    {
        _doctorService = doctorService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var doctors = await _doctorService.GetAllAsync();

        if (doctors is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var result = string.Empty;
        var role = string.Empty;
        if (context.User != null)
        {
            result = context.User.FindFirstValue(ClaimTypes.Name);
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }

        var doctorsResponse = doctors.ToDoctorsResponse();
        if (role.Equals("DOCTOR"))
        {
            await SendOkAsync(_anonymizationService.AnonymiseAllDoctorsExceptCurrent(doctorsResponse, result), ct);
            return;
        }
        await SendOkAsync(_anonymizationService.AnonymiseAllDoctors(doctorsResponse), ct);
    }
}
