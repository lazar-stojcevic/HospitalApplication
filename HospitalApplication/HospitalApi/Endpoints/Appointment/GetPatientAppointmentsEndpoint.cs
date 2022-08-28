using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Appointment;

[HttpGet("appointments/patient/{id:guid}"), Authorize(Roles = "ADMIN,PATIENT,DOCTOR")]
public class GetPatientAppointmentsEndpoint : Endpoint<GetPatientAppointmentsRequest, MultipleAppointmentsResponse>
{
    private readonly IAppointmentService _appointmentService;

    public GetPatientAppointmentsEndpoint(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public override async Task HandleAsync(GetPatientAppointmentsRequest req, CancellationToken ct)
    {
        var appointments = await _appointmentService.GetPatientsAppointmentsAsync(req.Id);

        if (appointments is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var appointmentsResponse = appointments.ToMultipleAppointmentResponse();
        await SendOkAsync(appointmentsResponse, ct);
    }
}

