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
    /*
    public override void Configure()
    {
        Get("appointments/patient/{id:guid}");
        AllowAnonymous();
    }
    */

    private readonly IAppointmentService _appointmentService;
    private readonly IAnonymizationService _anonymizationService;
    private readonly IPatientService _patientService;

    public GetPatientAppointmentsEndpoint(IAppointmentService appointmentService, IAnonymizationService anonymizationService, IPatientService patientService)
    {
        _appointmentService = appointmentService;
        _anonymizationService = anonymizationService;
        _patientService = patientService;
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

        if (role.Equals("ADMIN"))
        {
            await SendOkAsync(_anonymizationService.AnonymiseAppointments(appointmentsResponse),ct);
            return;
        }
        await SendOkAsync(appointmentsResponse, ct);
    }
}

