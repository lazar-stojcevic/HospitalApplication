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
    private readonly IAuthenticationService _authenticationService;

    public CreateDoctorEndpoint(IDoctorService doctorService, IAuthenticationService authenticationService)
    {
        _doctorService = doctorService;
        _authenticationService = authenticationService;
    }

    public override async Task HandleAsync(CreateDoctorRequest req, CancellationToken ct)
    {
        var doctor = req.ToDoctor();
        doctor.SetPassword(_authenticationService.HashPassword(req.Password));

        await _doctorService.CreateAsync(doctor);

        var doctorResponse = doctor.ToDoctorResponse();
        await SendCreatedAtAsync<GetDoctorEndpoint>(
            new { Id = doctor.Id.Value }, doctorResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}

