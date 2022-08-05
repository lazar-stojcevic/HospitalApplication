using FastEndpoints;
using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.ExportDatabase;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.ExportDatabase;

[HttpGet("export"), AllowAnonymous]
public class ExportDatabaseEndpoint : Endpoint<EmptyRequest, ExportDatabaseResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentService _appointmentService;
    private readonly IAccountService _accountService; 
    private readonly IPatientService _patientService;
    private readonly IAnonymizationService _anonymizationService;

    public ExportDatabaseEndpoint(
        IAppointmentService appointmentService,
        IAccountService accountService,
        IDoctorService doctorService,
        IPatientService patientService,
        IAnonymizationService anonymizationService
        )
    {
        _appointmentService = appointmentService;
        _accountService = accountService;
        _doctorService = doctorService;
        _patientService = patientService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var patients = await _patientService.GetAllAsync();
        var doctors = await _doctorService.GetAllAsync();
        var accounts = await _accountService.GetAllAsync();
        var appointments = await _appointmentService.GetAllAsync();

        await SendOkAsync(new ExportDatabaseResponse
        {
            Patients = _anonymizationService.AnonymisePatientsByMasking(patients) ?? new List<PatientDto>(),
            Doctors = _anonymizationService.AnonymiseDoctorsByMasking(doctors) ?? new List<DoctorDto>(),
            Accounts = _anonymizationService.AnonymiseAccountsByMasking(accounts) ?? new List<AccountDto>(),
            Appointments = _anonymizationService.AnonymiseAppointmentsByMasking(appointments) ?? new List<AppointmentDto>(),
        }, ct) ;
    }
}

