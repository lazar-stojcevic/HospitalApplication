using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Appointment;

[HttpGet("appointments/{id:guid}"), AllowAnonymous]
public class GetAppointmentEndpoint : Endpoint<GetAppointmentRequest, AppointmentResponse>
{
    private readonly IAppointmentService _appointmentService;

    public GetAppointmentEndpoint(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public override async Task HandleAsync(GetAppointmentRequest req, CancellationToken ct)
    {
        var appointment = await _appointmentService.GetAsync(req.Id);

        if (appointment is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var appointmentResponse = appointment.ToAppointmentResponse();
        await SendOkAsync(appointmentResponse, ct);
    }
}

