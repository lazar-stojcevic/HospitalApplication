using FastEndpoints;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.ExportDatabase;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.ExportDatabase;

[HttpGet("export-pseudonymization"), Authorize(Roles = "ADMIN")]
public class ExportDatabaseWithPseudonymizationEndpoint : Endpoint<EmptyRequest, ExportDatabaseResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentService _appointmentService;
    private readonly IAccountService _accountService; 
    private readonly IPatientService _patientService;
    private readonly IAccountantService _accountantService;
    private readonly IAdminService _adminService;
    private readonly IAnonymizationService _anonymizationService;

    public ExportDatabaseWithPseudonymizationEndpoint(
        IAppointmentService appointmentService,
        IAccountService accountService,
        IDoctorService doctorService,
        IPatientService patientService,
        IAnonymizationService anonymizationService,
        IAccountantService accountantService, IAdminService adminService)
    {
        _appointmentService = appointmentService;
        _accountService = accountService;
        _doctorService = doctorService;
        _patientService = patientService;
        _anonymizationService = anonymizationService;
        _accountantService = accountantService;
        _adminService = adminService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var patients = await _patientService.GetAllAsync();
        var doctors = await _doctorService.GetAllAsync(false);
        var accounts = await _accountService.GetAllAsync();
        var appointments = await _appointmentService.GetAllAsync();
        var accountants = await _accountantService.GetAllAsync();
        var admins = await _adminService.GetAllAsync();

        await SendOkAsync(new ExportDatabaseResponse
        {
            Patients = _anonymizationService.AnonymisePatientsByPseudonymization(patients)?.ToPatientsResponse() ?? new List<PatientResponse>(),
            Doctors = _anonymizationService.AnonymiseDoctorsByPseudonymization(doctors)?.ToDoctorsResponse() ?? new List<DoctorResponse>(),
            Accounts = _anonymizationService.AnonymiseAccountsByPseudonymization(accounts)?.ToAccountsResponse() ?? new List<AccountResponse>(),
            Appointments = _anonymizationService.AnonymiseAppointmentsByPseudonymization(appointments)?.ToMultipleAppointmentResponse() ?? new List<AppointmentResponse>(),
            Accountants = _anonymizationService.AnonymiseAccountantsByPseudonymization(accountants)?.ToAccountantsResponse() ?? new List<AccountantResponse>(),
            Admins = _anonymizationService.AnonymiseAdminsByPseudonymization(admins)?.ToAdminsResponse() ?? new List<AdminResponse>(),
        },
        ct) ;
    }
}

