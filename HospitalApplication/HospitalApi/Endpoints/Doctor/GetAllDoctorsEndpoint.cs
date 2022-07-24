using FastEndpoints;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Doctor;

[HttpGet("doctors"), AllowAnonymous]
public class GetAllDoctorsEndpoint : Endpoint<EmptyRequest, GetAllDoctorsResponse>
{
    private readonly IDoctorService _doctorService;

    public GetAllDoctorsEndpoint(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var doctors = await _doctorService.GetAllAsync();

        if (doctors is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var doctorsResponse = doctors.ToDoctorsResponse();
        await SendOkAsync(doctorsResponse, ct);
    }
}
