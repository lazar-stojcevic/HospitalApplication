using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Appointment;

[HttpGet("appointments/free/{patientid:guid}/{doctorId:guid}/{date}/{appointmentLength}"), AllowAnonymous]
public class GetFreeAppointmentsEndpoint : Endpoint<GetAvailableAppointmentsForDate, MultipleFreeAppointmentsResponse>
{
    private readonly IAppointmentService _appointmentService;

    public GetFreeAppointmentsEndpoint(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public override async Task HandleAsync(GetAvailableAppointmentsForDate req, CancellationToken ct)
    {
        var appointment = await _appointmentService.GetFreeAppointementsForPatientAndDoctor(req.PatientId, req.DoctorId, req.Date, req.AppointmentLength);
        await SendOkAsync(new MultipleFreeAppointmentsResponse { FreeAppointments = appointment ?? new List<FreeAppointmentResponse>() }, ct);
    }
}

