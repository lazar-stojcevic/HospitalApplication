using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
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
    AppointmentResponse AnonymiseAppointmentData(AppointmentResponse appointment);
    AccountantResponse AnonymiseAccountantData(Accountant accountant);
    AdminResponse AnonymiseAdminData(Admin admin);
    ICollection<AdminDto>? AnonymiseAdminsByMasking(ICollection<Admin>? admins);
    GetAllAccountantsResponse AnonymiseAllAccountantsExceptCurrent(GetAllAccountantsResponse allAccountants, string username);
    GetAllAdminsResponse AnonymiseAllAdminsExceptCurrent(GetAllAdminsResponse allAdmins, string username);
    GetAllDoctorsResponse AnonymiseAllDoctorsExceptCurrent(GetAllDoctorsResponse allDoctors, string username);
    MultipleAppointmentsResponse AnonymiseAppointments(MultipleAppointmentsResponse appointments);
    GetAllDoctorsResponse AnonymiseAllDoctors(GetAllDoctorsResponse allDoctors, bool forAdmin);
    DoctorResponse AnonymiseDoctorData(Doctor doctor);
    GetAllPatientsResponse AnonymiseAllPatientsExceptCurrent(GetAllPatientsResponse allPatients, string username);
    MultipleAppointmentsResponse AnonymiseAppointmentsForDoctor(MultipleAppointmentsResponse appointments);
    AccountsResponse AnonymiseMultipleAccounts(AccountsResponse accounts);
}

