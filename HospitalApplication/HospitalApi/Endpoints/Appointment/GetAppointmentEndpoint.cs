using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Appointment;

[HttpGet("appointments/{id:guid}"), Authorize(Roles = "DOCTOR,ADMIN,PATIENT")]
public class GetAppointmentEndpoint : Endpoint<GetAppointmentRequest, AppointmentResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAnonymizationService _anonymizationService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public GetAppointmentEndpoint(IAppointmentService appointmentService, IAnonymizationService anonymizationService, IPatientService patientService, IDoctorService doctorService)
    {
        _appointmentService = appointmentService;
        _anonymizationService = anonymizationService;
        _patientService = patientService;
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(GetAppointmentRequest req, CancellationToken ct)
    {
        var appointment = await _appointmentService.GetAsync(req.Id);

        if (appointment is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var username = string.Empty;
        var role = string.Empty;

        var patient = await _patientService.GetAsync(appointment.PatientId.Value);
        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        if (context.User != null)
        {
            username = context.User.FindFirstValue(ClaimTypes.Name);
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }
        if (!patient.Username.Equals(username) && role.Equals("PATIENT"))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var appointmentResponse = appointment.ToAppointmentResponse();

        var doctor = await _doctorService.GetAsync(appointment.DoctorId.Value);
        appointmentResponse.DoctorName = $"{ doctor.FirstName} { doctor.Surname}";
        appointmentResponse.DoctorName = doctor.MedicalSpeciality.Value;

        if (role.Equals("ADMIN"))
        {
            await SendOkAsync(_anonymizationService.AnonymiseAppointmentData(appointmentResponse),ct);
            return;
        }
        await SendOkAsync(appointmentResponse, ct);
    }
}

