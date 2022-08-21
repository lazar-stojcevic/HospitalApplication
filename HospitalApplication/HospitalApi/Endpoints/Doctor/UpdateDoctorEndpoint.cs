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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateDoctorEndpoint(IDoctorService doctorService, IHttpContextAccessor httpContextAccessor)
    {
        _doctorService = doctorService;
        _httpContextAccessor = httpContextAccessor;
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
        doctor.SetPassword(existingDoctor.Password);
        await _doctorService.UpdateAsync(doctor);

        var doctorResponse = doctor.ToDoctorResponse();
        await SendOkAsync(doctorResponse, ct);
    }
}

