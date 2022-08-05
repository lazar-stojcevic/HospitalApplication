using HospitalApi.Contracts.Data;
using HospitalApi.Domain;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services;
public class AnonymizationService : IAnonymizationService
{
    public ICollection<AccountDto>? AnonymiseAccountsByMasking(ICollection<Account>? accounts)
    {
        var retVal = new List<AccountDto>();

        retVal.AddRange(accounts.Select(account => new AccountDto
        {
            Id = account.Id.Value.ToString(),
            AccountNumber = "*******************",
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
            Report = appointment.Report?.Value ?? string.Empty,
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
            Email = "******@*****.com",
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = "*******",
            Surname = "****************",
            MedicalSpeciality = doctor.MedicalSpeciality.Value.ToString(),
            PersonalNumber = "*************",
            PhoneNumber = "*****-*****",
            Password = "/",
            Username = "***********"
        }));
        return retVal;
    }

    public ICollection<PatientDto>? AnonymisePatientsByMasking(ICollection<Patient>? patients)
    {
        var retVal = new List<PatientDto>();

        Random gen = new Random();

        var range = 45 * 365; //45 years  

        retVal.AddRange(patients.Select(patient => new PatientDto
        {
            Id = patient.Id.Value.ToString(),
            Email = "*********@*****.com",
            DateOfBirth = DateTime.Today.AddDays(-gen.Next(range)).AddYears(24),
            FirstName = "*******",
            Surname = "****************",
            PersonalNumber = "*************",
            PhoneNumber = "*****-*****",
            Password = "/",
            Username = "***********",
            AccountId = patient.AccountId.Value.ToString(),
            Adress = "******* *******",
            BloodType = patient.BloodType.Value,
            Gender = patient.Gender.Value,
            Height = patient.Height.Value,
            Weight = patient.Weight.Value,
        }));
        return retVal;
    }
}

