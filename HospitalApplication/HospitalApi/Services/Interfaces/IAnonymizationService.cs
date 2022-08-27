using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAnonymizationService
{
    ICollection<DoctorDto>? AnonymiseDoctorsByMasking(ICollection<Doctor>? doctors);
    ICollection<PatientDto>? AnonymisePatientsByMasking(ICollection<Patient>? patients);
    ICollection<AccountDto>? AnonymiseAccountsByMasking(ICollection<Account>? accounts);
    ICollection<AppointmentDto>? AnonymiseAppointmentsByMasking(ICollection<Appointment>? appointments);
    ICollection<AccountantDto>? AnonymiseAccountantsByMasking(ICollection<Accountant>? accountants);
    PatientResponse AnonymisePatiendData(Patient patient);
    AccountResponse AnonymiseAccountData(Account account);
    AppointmentResponse AnonymiseAppointmentData(Appointment appointment);
}

