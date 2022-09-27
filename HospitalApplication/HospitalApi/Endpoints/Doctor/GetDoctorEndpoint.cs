using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Doctor;

[HttpGet("doctors/{id:guid}"), Authorize]
public class GetDoctorEndpoint : Endpoint<GetDoctorRequest, DoctorResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IAnonymizationService _anonymizationService;

    public GetDoctorEndpoint(IDoctorService doctorService, IAnonymizationService anonymizationService)
    {
        _doctorService = doctorService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(GetDoctorRequest req, CancellationToken ct)
    {
        var doctor = await _doctorService.GetAsync(req.Id);

        if (doctor is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var username = string.Empty;
        var role = string.Empty;
        if (context.User != null)
        {
            username = context.User.FindFirstValue("Username");
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }

        if (role.Equals("DOCTOR") && doctor.Username.ToString().Equals(username))
        {
            await SendOkAsync(doctor.ToDoctorResponse(), ct);
        }
        else
        {
            await SendOkAsync(_anonymizationService.AnonymiseDoctorData(doctor),ct);
        }
    }
}

