using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Doctor;

[HttpPut("doctors/{id:guid}"), AllowAnonymous]
public class UpdateDoctorEndpoint : Endpoint<UpdateDoctorRequest, DoctorResponse>
{
    private readonly IDoctorService _doctorService;

    public UpdateDoctorEndpoint(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(UpdateDoctorRequest req, CancellationToken ct)
    {
        var existingDoctor = await _doctorService.GetAsync(Guid.Parse(req.Id));

        if (existingDoctor is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var doctor = req.ToDoctor();
        await _doctorService.UpdateAsync(doctor);

        var doctorResponse = doctor.ToDoctorResponse();
        await SendOkAsync(doctorResponse, ct);
    }
}

