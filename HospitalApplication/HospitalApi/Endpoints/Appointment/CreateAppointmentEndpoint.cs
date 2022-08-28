using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Appointment;

[HttpPost("appointments"), Authorize(Roles = "DOCTOR,PATIENT")]
public class CreateAppointmentEndpoint : Endpoint<CreateAppointmentRequest, AppointmentResponse>
{
    private readonly IAppointmentService _appointmentService;

    public CreateAppointmentEndpoint(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public override async Task HandleAsync(CreateAppointmentRequest req, CancellationToken ct)
    {
        var appointment = req.ToAppointment();

        await _appointmentService.CreateAsync(appointment);

        var appointmentResponse = appointment.ToAppointmentResponse();
        await SendCreatedAtAsync<GetAppointmentEndpoint>(
            new { Id = appointment.Id.Value }, appointmentResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}

