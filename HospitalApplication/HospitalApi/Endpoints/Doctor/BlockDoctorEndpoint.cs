using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Doctor;

[HttpPut("doctors/block/{id:guid}"), Authorize(Roles = "ADMIN")]
public class BlockDoctorEndpoint : Endpoint<BlockDoctorRequest, bool>
{
    private readonly IDoctorService _doctorService;

    public BlockDoctorEndpoint(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(BlockDoctorRequest req, CancellationToken ct)
    {
        var doctor = await _doctorService.GetAsync(req.Id, true);

        if (doctor is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        doctor.IsActive = false;
        doctor.SetPassword(doctor.Password);
        await _doctorService.UpdateAsync(doctor);

        await SendOkAsync(true, ct);
    }
}

