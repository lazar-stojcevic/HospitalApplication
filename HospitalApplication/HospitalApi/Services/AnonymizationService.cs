using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Domain;
using HospitalApi.Services.Interfaces;
using System.Text.RegularExpressions;

namespace HospitalApi.Services;
public class AnonymizationService : IAnonymizationService
{

    public static Random gen = new Random();
    public ICollection<AccountDto>? AnonymiseAccountsByMasking(ICollection<Account>? accounts)
    {
        var retVal = new List<AccountDto>();

        retVal.AddRange(accounts.Select(account => new AccountDto
        {
            Id = account.Id.Value.ToString(),
            AccountNumber = AnonymiseString(account.AccountNumber.Value),
            Balance = account.Balance.Value,
            PatientId = account.PatientId.Value.ToString(),
        }));

        return retVal;
    }

    public ICollection<AppointmentDto>? AnonymiseAppointmentsByMasking(ICollection<Appointment>? appointments)
    {
        var retVal = new List<AppointmentDto>();

        retVal.AddRange(appointments.Select(appointment => new AppointmentDto
        {
            Id = appointment.Id.Value.ToString(),
            PatientId = appointment.PatientId.Value.ToString(),
            DoctorId = appointment.DoctorId.Value.ToString(),
            EndTime = appointment.EndTime.Value,
            StartTime = appointment.StartTime.Value,
            Price = appointment.Price?.Value ?? 0,
            Report = AnonymiseString(appointment.Report?.Value ?? string.Empty),
        }));

        return retVal;
    }

    public ICollection<DoctorDto>? AnonymiseDoctorsByMasking(ICollection<Doctor>? doctors)
    {
        var retVal = new List<DoctorDto>();

        Random gen = new Random();

        var range = 45 * 365; //45 years  

        retVal.AddRange(doctors.Select(doctor => new DoctorDto
        {
            Id = doctor.Id.Value.ToString(),
            Email = AnonymiseString(doctor.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(doctor.FirstName.Value),
            Surname = AnonymiseString(doctor.Surname.Value),
            MedicalSpeciality = doctor.MedicalSpeciality.Value.ToString(),
            PersonalNumber = AnonymiseString(doctor.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(doctor.PhoneNumber.Value),
            Password = "/",
            Username = doctor.Username.Value,
        }));
        return retVal;
    }

    public ICollection<PatientDto>? AnonymisePatientsByMasking(ICollection<Patient>? patients)
    {
        var retVal = new List<PatientDto>();

        var range = 45 * 365; //45 years  

        retVal.AddRange(patients.Select(patient => new PatientDto
        {
            Id = patient.Id.Value.ToString(),
            Email = AnonymiseString(patient.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(patient.FirstName.Value),
            Surname = AnonymiseString(patient.Surname.Value),
            PersonalNumber = AnonymiseString(patient.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(patient.PhoneNumber.Value),
            Password = "/",
            Username = patient.Username.Value,
            AccountId = patient.AccountId.Value.ToString(),
            Adress = AnonymiseString(patient.Adress.Value),
            BloodType = patient.BloodType.Value,
            Gender = patient.Gender.Value,
            Height = patient.Height.Value,
            Weight = patient.Weight.Value,
        }));
        return retVal;
    }

    public ICollection<AccountantDto>? AnonymiseAccountantsByMasking(ICollection<Accountant>? accountants)
    {
        var retVal = new List<AccountantDto>();

        var range = 45 * 365; //45 years  

        retVal.AddRange(accountants.Select(accountant => new AccountantDto
        {
            Id = accountant.Id.Value.ToString(),
            Email = AnonymiseString(accountant.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(accountant.FirstName.Value),
            Surname = AnonymiseString(accountant.Surname.Value),
            PersonalNumber = AnonymiseString(accountant.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(accountant.PhoneNumber.Value),
            Password = "/",
            Username = accountant.Username.Value
        }));
        return retVal;
    }

    public ICollection<AdminDto>? AnonymiseAdminsByMasking(ICollection<Admin>? admins)
    {
        var retVal = new List<AdminDto>();

        var range = 45 * 365; //45 years  

        retVal.AddRange(admins.Select(admin => new AdminDto
        {
            Id = admin.Id.Value.ToString(),
            Email = AnonymiseString(admin.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(admin.FirstName.Value),
            Surname = AnonymiseString(admin.Surname.Value),
            PersonalNumber = AnonymiseString(admin.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(admin.PhoneNumber.Value),
            Password = "/",
            Username = admin.Username.Value
        }));
        return retVal;
    }

    public PatientResponse AnonymisePatiendData(Patient patient)
    {
        Random gen = new Random();
        var range = 45 * 365; //45 years  
        return new PatientResponse
        {
            Id = Guid.Parse(patient.Id.Value.ToString()),
            Email = AnonymiseString(patient.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(patient.FirstName.Value),
            Surname = AnonymiseString(patient.Surname.Value),
            PersonalNumber = AnonymiseString(patient.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(patient.PhoneNumber.Value),
            Username = patient.Username.Value,
            AccountId = Guid.Parse(patient.AccountId.Value.ToString()),
            Adress = AnonymiseString(patient.Adress.Value),
            BloodType = patient.BloodType.Value,
            Gender = patient.Gender.Value,
            Height = patient.Height.Value,
            Weight = patient.Weight.Value,

        };
    }

    public AccountResponse AnonymiseAccountData(Account account)
    {
        return new AccountResponse
        {
            Id = Guid.Parse(account.Id.Value.ToString()),
            AccountNumber = AnonymiseString(account.AccountNumber.Value),
            Balance = account.Balance.Value,
            PatientId = Guid.Parse(account.PatientId.Value.ToString()),
        };
    }
    
    public AppointmentResponse AnonymiseAppointmentData(Appointment appointment)
    {
        return new AppointmentResponse
        {
            Id = Guid.Parse(appointment.Id.Value.ToString()),
            PatientId = Guid.Parse(appointment.PatientId.Value.ToString()),
            DoctorId = Guid.Parse(appointment.DoctorId.Value.ToString()),
            EndTime = appointment.EndTime.Value,
            StartTime = appointment.StartTime.Value,
            Price = appointment.Price?.Value ?? 0,
            Report = AnonymiseString(appointment.Report?.Value ?? string.Empty),
        };
    }

    public AccountantResponse AnonymiseAccountantData(Accountant accountant)
    {
        var range = 45 * 365;

        return new AccountantResponse
        {
            Id = accountant.Id.Value,
            Email = AnonymiseString(accountant.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(accountant.FirstName.Value),
            Surname = AnonymiseString(accountant.Surname.Value),
            PersonalNumber = AnonymiseString(accountant.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(accountant.PhoneNumber.Value),
            Username = accountant.Username.Value
        };
    }

    public AdminResponse AnonymiseAdminData(Admin admin)
    {
        var range = 45 * 365;

        return new AdminResponse
        {
            Id = admin.Id.Value,
            Email = AnonymiseString(admin.Email.Value),
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = AnonymiseString(admin.FirstName.Value),
            Surname = AnonymiseString(admin.Surname.Value),
            PersonalNumber = AnonymiseString(admin.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(admin.PhoneNumber.Value),
            Username = admin.Username.Value
        };
    }

    private string AnonymiseString(string report)
    {
        return new Regex("\\S").Replace(report, "*");
    }
}

