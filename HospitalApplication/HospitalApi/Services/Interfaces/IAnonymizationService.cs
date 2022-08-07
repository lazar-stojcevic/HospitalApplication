using HospitalApi.Contracts.Data;
using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAnonymizationService
{
    ICollection<DoctorDto>? AnonymiseDoctorsByMasking(ICollection<Doctor>? doctors);
    ICollection<PatientDto>? AnonymisePatientsByMasking(ICollection<Patient>? patients);
    ICollection<AccountDto>? AnonymiseAccountsByMasking(ICollection<Account>? accounts);
    ICollection<AppointmentDto>? AnonymiseAppointmentsByMasking(ICollection<Appointment>? appointments);
    ICollection<AccountantDto>? AnonymiseAccountantsByMasking(ICollection<Accountant>? accountants);
}

