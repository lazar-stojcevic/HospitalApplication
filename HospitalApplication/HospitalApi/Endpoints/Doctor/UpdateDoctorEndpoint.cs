using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Doctor;

[HttpPut("doctors/{id:guid}"), Authorize(Roles = "DOCTOR")]
public class UpdateDoctorEndpoint : Endpoint<UpdateDoctorRequest, DoctorResponse>
{
    private readonly IDoctorService _doctorService;

    public UpdateDoctorEndpoint(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(UpdateDoctorRequest req, CancellationToken ct)
    {
        var context = HttpContext;
        var username = string.Empty;
        if (context.User != null)
        {
            username = context.User.FindFirstValue("Username");
        }

        var existingDoctor = await _doctorService.GetAsync(Guid.Parse(req.Id), true);

        if (existingDoctor is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!existingDoctor.Username.ToString().Equals(username))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var doctor = req.ToDoctor();
        doctor.SetPassword(existingDoctor.Password);
        await _doctorService.UpdateAsync(doctor);

        var doctorResponse = doctor.ToDoctorResponse();
        await SendOkAsync(doctorResponse, ct);
    }
}

