using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Doctor;

[HttpPost("doctors"), AllowAnonymous]
public class CreateDoctorEndpoint : Endpoint<CreateDoctorRequest, DoctorResponse>
{
    private readonly IDoctorService _doctorService;

    public CreateDoctorEndpoint(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(CreateDoctorRequest req, CancellationToken ct)
    {
        var doctor = req.ToDoctor();

        await _doctorService.CreateAsync(doctor);

        var doctorResponse = doctor.ToDoctorResponse();
        await SendCreatedAtAsync<GetDoctorEndpoint>(
            new { Id = doctor.Id.Value }, doctorResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}

