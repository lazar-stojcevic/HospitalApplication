using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Appointment;

[HttpGet("appointments/doctor/future/{id:guid}"), Authorize(Roles = "DOCTOR,ADMIN")]
public class GetDoctorsFutureAppointmets : Endpoint<GetDoctorAppointmentsRequest, MultipleAppointmentsResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;
    private readonly IAnonymizationService _anonymizationService;

    public GetDoctorsFutureAppointmets(IAppointmentService appointmentService, IPatientService patientService, IAnonymizationService anonymizationService)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(GetDoctorAppointmentsRequest req, CancellationToken ct)
    {
        var appointments = await _appointmentService.GetDoctorsFutureAppointmentsAsync(req.Id);

        if (appointments is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var role = string.Empty;
        if (context.User != null)
        {
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }

        var patients = await _patientService.GetAllAsync();

        var appointmentsResponse = appointments.ToMultipleAppointmentResponse();

        foreach(var appointment in appointmentsResponse.Appointments)
        {
            var patient = patients.First(x => x.Id.Value == appointment.PatientId);
            appointment.PatientName = $"{patient.FirstName} {patient.Surname}";
        }

        if (role.Equals("ADMIN"))
        {
            appointmentsResponse = _anonymizationService.AnonymiseAppointments(appointmentsResponse);
        }

        await SendOkAsync(appointmentsResponse, ct);
    }
}

