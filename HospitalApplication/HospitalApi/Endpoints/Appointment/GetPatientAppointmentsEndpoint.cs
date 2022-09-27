using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Appointment;
[HttpGet("appointments/patient/{id:guid}"), Authorize]
public class GetPatientAppointmentsEndpoint : Endpoint<GetPatientAppointmentsRequest, MultipleAppointmentsResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAnonymizationService _anonymizationService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public GetPatientAppointmentsEndpoint(IAppointmentService appointmentService, IAnonymizationService anonymizationService, IPatientService patientService, IDoctorService doctorService)
    {
        _appointmentService = appointmentService;
        _anonymizationService = anonymizationService;
        _patientService = patientService;
        _doctorService = doctorService;
    }

    public override async Task HandleAsync(GetPatientAppointmentsRequest req, CancellationToken ct)
    {
        var appointments = await _appointmentService.GetPatientsAppointmentsAsync(req.Id);

        if (appointments is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var username = string.Empty;
        var role = string.Empty;

        var patient = await _patientService.GetAsync(req.Id);
        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        if (context.User != null)
        {
            username = context.User.FindFirstValue("Username"); ;
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }

        if (!username.Equals(patient.Username.Value) && role.Equals("PATIENT"))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var appointmentsResponse = appointments.ToMultipleAppointmentResponse();

        var doctors = await _doctorService.GetAllAsync();

        foreach (var appointment in appointmentsResponse.Appointments)
        {
            var doctor = doctors!.Where(x => x.Id.Value == appointment.DoctorId).FirstOrDefault();
            appointment.DoctorName = $"{doctor.FirstName.Value} {doctor.Surname.Value}";
            appointment.DoctorSpeciality = $"{doctor.MedicalSpeciality.Value}";
        }

        if (role.Equals("ADMIN"))
        {
            await SendOkAsync(_anonymizationService.AnonymiseAppointments(appointmentsResponse),ct);
            return;
        }else if (role.Equals("DOCTOR"))
        {
            await SendOkAsync(_anonymizationService.AnonymiseAppointmentsForDoctor(appointmentsResponse), ct);
            return;
        }
        await SendOkAsync(appointmentsResponse, ct);
    }
}

