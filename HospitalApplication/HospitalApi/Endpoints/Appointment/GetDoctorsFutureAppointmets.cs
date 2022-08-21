using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Appointment;

[HttpGet("appointments/doctor/future/{id:guid}"), AllowAnonymous]
public class GetDoctorsFutureAppointmets : Endpoint<GetDoctorAppointmentsRequest, MultipleAppointmentsResponse>
{
    private readonly IAppointmentService _appointmentService;

    public GetDoctorsFutureAppointmets(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public override async Task HandleAsync(GetDoctorAppointmentsRequest req, CancellationToken ct)
    {
        var appointments = await _appointmentService.GetDoctorsFutureAppointmentsAsync(req.Id);

        if (appointments is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var appointmentsResponse = appointments.ToMultipleAppointmentResponse();
        await SendOkAsync(appointmentsResponse, ct);
    }
}

