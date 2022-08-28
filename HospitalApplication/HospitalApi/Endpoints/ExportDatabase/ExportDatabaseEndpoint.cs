using FastEndpoints;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.ExportDatabase;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.ExportDatabase;

[HttpGet("export"), Authorize(Roles = "ADMIN")]
public class ExportDatabaseEndpoint : Endpoint<EmptyRequest, ExportDatabaseResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentService _appointmentService;
    private readonly IAccountService _accountService; 
    private readonly IPatientService _patientService;
    private readonly IAccountantService _accountantService;
    private readonly IAnonymizationService _anonymizationService;

    public ExportDatabaseEndpoint(
        IAppointmentService appointmentService,
        IAccountService accountService,
        IDoctorService doctorService,
        IPatientService patientService,
        IAnonymizationService anonymizationService,
        IAccountantService accountantService)
    {
        _appointmentService = appointmentService;
        _accountService = accountService;
        _doctorService = doctorService;
        _patientService = patientService;
        _anonymizationService = anonymizationService;
        _accountantService = accountantService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var patients = await _patientService.GetAllAsync();
        var doctors = await _doctorService.GetAllAsync();
        var accounts = await _accountService.GetAllAsync();
        var appointments = await _appointmentService.GetAllAsync();
        var accountants = await _accountantService.GetAllAsync();

        await SendOkAsync(new ExportDatabaseResponse
        {
            Patients = _anonymizationService.AnonymisePatientsByMasking(patients)?.ToPatientsResponse() ?? new List<PatientResponse>(),
            Doctors = _anonymizationService.AnonymiseDoctorsByMasking(doctors)?.ToDoctorsResponse() ?? new List<DoctorResponse>(),
            Accounts = _anonymizationService.AnonymiseAccountsByMasking(accounts)?.ToAccountsResponse() ?? new List<AccountResponse>(),
            Appointments = _anonymizationService.AnonymiseAppointmentsByMasking(appointments)?.ToMultipleAppointmentResponse() ?? new List<AppointmentResponse>(),
            Accountants = _anonymizationService.AnonymiseAccountantsByMasking(accountants)?.ToAccountantsResponse() ?? new List<AccountantResponse>(),
        },
        ct) ;
    }
}

