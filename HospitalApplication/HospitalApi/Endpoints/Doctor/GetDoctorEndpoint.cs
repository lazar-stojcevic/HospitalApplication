using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Doctor;

[HttpGet("doctors/{id:guid}"), AllowAnonymous]
public class GetDoctorEndpoint : Endpoint<GetDoctorRequest, DoctorResponse>
{
    private readonly IDoctorService _doctorService;

    public GetDoctorEndpoint(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(GetDoctorRequest req, CancellationToken ct)
    {
        var doctor = await _doctorService.GetAsync(req.Id);

        if (doctor is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var doctorResponse = doctor.ToDoctorResponse();
        await SendOkAsync(doctorResponse, ct);
    }
}

