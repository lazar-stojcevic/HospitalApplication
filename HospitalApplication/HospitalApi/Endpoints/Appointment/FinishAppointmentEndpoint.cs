using FastEndpoints;
using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Appointment;
[HttpPut("appointments/finish"), AllowAnonymous]
public class FinishAppointmentEndpoint : Endpoint<FinishAppointmentRequest, AppointmentResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAccountService _accountService;
    private readonly IPatientService _patientService;

    public FinishAppointmentEndpoint(IAppointmentService appointmentService, IAccountService accountService, IPatientService patientService)
    {
        _appointmentService = appointmentService;
        _accountService = accountService;
        _patientService = patientService;
    }

    public override async Task HandleAsync(FinishAppointmentRequest req, CancellationToken ct)
    {
        var appointment = await _appointmentService.GetAsync(req.Id);

        if (appointment == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (appointment.Report?.Value != null)
        {
            await SendForbiddenAsync();
            return;
        }

        var patient = await _patientService.GetAsync(appointment.PatientId.Value);

        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var existingAccount = await _accountService.GetAsync(patient.AccountId.Value);

        if (existingAccount is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        appointment.SetReportAndPrice(req.Report, req.Price);

        await _accountService.UpdateAsync(existingAccount, -req.Price);

        await _appointmentService.UpdateAsync(appointment);

        var appointmentResponse = appointment.ToAppointmentResponse();
        await SendCreatedAtAsync<GetAppointmentEndpoint>(
            new { Id = appointment.Id.Value }, appointmentResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}

