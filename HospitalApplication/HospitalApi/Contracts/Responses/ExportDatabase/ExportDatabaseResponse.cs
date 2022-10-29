using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;

namespace HospitalApi.Contracts.Responses.ExportDatabase;

public class ExportDatabaseResponse
{
    public IEnumerable<PatientResponse> Patients { get; init; } = Enumerable.Empty<PatientResponse>();
    public IEnumerable<DoctorResponse> Doctors { get; init; } = Enumerable.Empty<DoctorResponse>();
    public IEnumerable<AppointmentResponse> Appointments { get; init; } = Enumerable.Empty<AppointmentResponse>();
    public IEnumerable<AccountResponse> Accounts { get; init; } = Enumerable.Empty<AccountResponse>();
    public IEnumerable<AccountantResponse> Accountants { get; init; } = Enumerable.Empty<AccountantResponse>();
    public IEnumerable<AdminResponse> Admins { get; init; } = Enumerable.Empty<AdminResponse>();
}

