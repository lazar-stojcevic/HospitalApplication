using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Domain;
using HospitalApi.Pseudonymization;
using HospitalApi.Services.Interfaces;
using LoremNET;
using System.Linq;
using System.Text.RegularExpressions;

namespace HospitalApi.Services;
public class AnonymizationService : IAnonymizationService
{
    public static Random gen = new Random();


    public ICollection<AccountDto>? AnonymiseAccountsByPseudonymization(ICollection<Account>? accounts)
    {
        var retVal = new List<AccountDto>();

        retVal.AddRange(accounts!.Select(account => new AccountDto
        {
            Id = account.Id.Value.ToString(),
            AccountNumber = $"123{gen.Next(1000, 9999)}{gen.Next(100, 999)}{gen.Next(100, 999)}",
            Balance = account.Balance.Value,
            PatientId = account.PatientId.Value.ToString(),
        }));

        return retVal;
    }

    public ICollection<AppointmentDto>? AnonymiseAppointmentsByPseudonymization(ICollection<Appointment>? appointments)
    {
        var retVal = new List<AppointmentDto>();

        retVal.AddRange(appointments!.Select(appointment => new AppointmentDto
        {
            Id = appointment.Id.Value.ToString(),
            PatientId = appointment.PatientId.Value.ToString(),
            DoctorId = appointment.DoctorId.Value.ToString(),
            EndTime = appointment.EndTime.Value,
            StartTime = appointment.StartTime.Value,
            Price = appointment.Price?.Value ?? 0,
            // 5 to 12 words in sentence, in 2 to 10 sentences
            Report = appointment.Report is null ? "" : Lorem.Paragraph(5, 12, 2, 10),
        }));

        return retVal;
    }

    public ICollection<DoctorDto>? AnonymiseDoctorsByPseudonymization(ICollection<Doctor>? doctors)
    {
        var retVal = new List<DoctorDto>();

        retVal.AddRange(doctors!.Select(doctor => new DoctorDto
        {
            Id = doctor.Id.Value.ToString(),
            Email = Lorem.Email(),
            DateOfBirth = Lorem.DateTime(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
            FirstName = GetRandomFirstName(),
            Surname = GetRadnomSurname(),
            MedicalSpeciality = doctor.MedicalSpeciality.Value.ToString(),
            PersonalNumber = GetRandomPersonalNumber(),
            PhoneNumber = GetRandomPhoneNumber(),
            Password = "password",
            Username = GetRandomUsername(),
            IsActive = doctor.IsActive,
        })); ;
        return retVal;
    }

    public ICollection<PatientDto>? AnonymisePatientsByPseudonymization(ICollection<Patient>? patients)
    {
        var retVal = new List<PatientDto>();
        retVal.AddRange(patients!.Select(patient => new PatientDto
        {
            Id = patient.Id.Value.ToString(),
            Email = Lorem.Email(),
            DateOfBirth = Lorem.DateTime(new DateTime(1950, 1, 1), new DateTime(2020, 1, 1)),
            FirstName = patient.Gender.Value.Equals("Male") ? GetRandomMaleFirstName() : GetRadnomFemaleFirstName(),
            Surname = GetRadnomSurname(),
            PersonalNumber = GetRandomPersonalNumber(),
            PhoneNumber = GetRandomPhoneNumber(),
            Password = "password",
            Username = GetRandomUsername(),
            AccountId = patient.AccountId.Value.ToString(),
            Adress = GetRandomAddress(),
            BloodType = patient.BloodType.Value,
            Gender = patient.Gender.Value,
            Height = patient.Height.Value,
            Weight = patient.Weight.Value,
            IsActive = patient.IsActive
        }));
        return retVal;
    }

    public ICollection<AccountantDto>? AnonymiseAccountantsByPseudonymization(ICollection<Accountant>? accountants)
    {
        var retVal = new List<AccountantDto>();
        retVal.AddRange(accountants!.Select(accountant => new AccountantDto
        {
            Id = accountant.Id.Value.ToString(),
            Email = Lorem.Email(),
            DateOfBirth = Lorem.DateTime(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
            FirstName = GetRandomFirstName(),
            Surname = GetRadnomSurname(),
            PersonalNumber = GetRandomPersonalNumber(),
            PhoneNumber = GetRandomPhoneNumber(),
            Password = "password",
            Username = GetRandomUsername()
        }));
        return retVal;
    }

    public ICollection<AdminDto>? AnonymiseAdminsByPseudonymization(ICollection<Admin>? admins)
    {
        var retVal = new List<AdminDto>();
        retVal.AddRange(admins!.Select(admin => new AdminDto
        {
            Id = admin.Id.Value.ToString(),
            Email = Lorem.Email(),
            DateOfBirth = Lorem.DateTime(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
            FirstName = GetRandomFirstName(),
            Surname = GetRadnomSurname(),
            PersonalNumber = GetRandomPersonalNumber(),
            PhoneNumber = GetRandomPhoneNumber(),
            Password = "password",
            Username = GetRandomUsername(),
        }));
        return retVal;
    }

    public ICollection<AccountDto>? AnonymiseAccountsByMasking(ICollection<Account>? accounts)
    {
        var retVal = new List<AccountDto>();

        retVal.AddRange(accounts!.Select(account => new AccountDto
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

        retVal.AddRange(appointments!.Select(appointment => new AppointmentDto
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

        retVal.AddRange(doctors!.Select(doctor => new DoctorDto
        {
            Id = doctor.Id.Value.ToString(),
            Email = AnonymiseString(doctor.Email.Value),
            DateOfBirth = doctor.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            FirstName = AnonymiseString(doctor.FirstName.Value),
            Surname = AnonymiseString(doctor.Surname.Value),
            MedicalSpeciality = doctor.MedicalSpeciality.Value.ToString(),
            PersonalNumber = AnonymiseString(doctor.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(doctor.PhoneNumber.Value),
            Password = "/",
            Username = doctor.Username.Value,
            IsActive = doctor.IsActive,
        }));
        return retVal;
    }

    public ICollection<PatientDto>? AnonymisePatientsByMasking(ICollection<Patient>? patients)
    {
        var retVal = new List<PatientDto>();
        retVal.AddRange(patients!.Select(patient => new PatientDto
        {
            Id = patient.Id.Value.ToString(),
            Email = AnonymiseString(patient.Email.Value),
            DateOfBirth = patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
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
            IsActive = patient.IsActive
        }));
        return retVal;
    }

    public ICollection<AccountantDto>? AnonymiseAccountantsByMasking(ICollection<Accountant>? accountants)
    {
        var retVal = new List<AccountantDto>();
        retVal.AddRange(accountants!.Select(accountant => new AccountantDto
        {
            Id = accountant.Id.Value.ToString(),
            Email = AnonymiseString(accountant.Email.Value),
            DateOfBirth = accountant.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
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
        retVal.AddRange(admins!.Select(admin => new AdminDto
        {
            Id = admin.Id.Value.ToString(),
            Email = AnonymiseString(admin.Email.Value),
            DateOfBirth = admin.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            FirstName = AnonymiseString(admin.FirstName.Value),
            Surname = AnonymiseString(admin.Surname.Value),
            PersonalNumber = AnonymiseString(admin.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(admin.PhoneNumber.Value),
            Password = "/",
            Username = admin.Username.Value
        }));
        return retVal;
    }

    public DoctorResponse AnonymiseDoctorData(Doctor doctor)
    {
        return new DoctorResponse
        {
            Id = doctor.Id.Value,
            Email = AnonymiseString(doctor.Email.Value),
            DateOfBirth = doctor.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            FirstName = doctor.FirstName.Value,
            Surname = doctor.Surname.Value,
            PersonalNumber = AnonymiseString(doctor.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(doctor.PhoneNumber.Value),
            Username = doctor.Username.Value,
            MedicalSpeciality = doctor.MedicalSpeciality.Value,
            IsActive = doctor.IsActive
        };
    }

    public DoctorResponse AnonymiseDoctorResponse(DoctorResponse doctor, bool forAdmin)
    {
        return new DoctorResponse
        {
            Id = doctor.Id,
            Email = AnonymiseString(doctor.Email),
            DateOfBirth = doctor.DateOfBirth,
            FirstName = doctor.FirstName,
            Surname = doctor.Surname,
            PersonalNumber = AnonymiseString(doctor.PersonalNumber),
            PhoneNumber = AnonymiseString(doctor.PhoneNumber),
            Username = forAdmin ? doctor.Username : AnonymiseString(doctor.Username),
            MedicalSpeciality = doctor.MedicalSpeciality,
            IsActive = doctor.IsActive
        };
    }

    public PatientResponse AnonymisePatiendData(Patient patient)
    {
        return new PatientResponse
        {
            Id = Guid.Parse(patient.Id.Value.ToString()),
            Email = AnonymiseString(patient.Email.Value),
            DateOfBirth = patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
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
            IsActive = patient.IsActive
        };
    }

    public PatientResponse AnonymisePatientResponse(PatientResponse patient)
    {
        return new PatientResponse
        {
            Id = patient.Id,
            Email = AnonymiseString(patient.Email),
            DateOfBirth = patient.DateOfBirth,
            FirstName = AnonymiseString(patient.FirstName),
            Surname = AnonymiseString(patient.Surname),
            PersonalNumber = AnonymiseString(patient.PersonalNumber),
            PhoneNumber = AnonymiseString(patient.PhoneNumber),
            Username = patient.Username,
            AccountId = patient.AccountId,
            Adress = AnonymiseString(patient.Adress),
            BloodType = patient.BloodType,
            Gender = patient.Gender,
            Height = patient.Height,
            Weight = patient.Weight,
            IsActive = patient.IsActive
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

    public AccountsResponse AnonymiseMultipleAccounts(AccountsResponse accounts)
    {
        var retVal = new AccountsResponse();
        foreach (var account in accounts.Accounts)
        {
            retVal.Accounts.Add(new AccountResponse()
            {
                Id = account.Id,
                AccountNumber = AnonymiseString(account.AccountNumber),
                PatientId = account.PatientId,
                Balance = account.Balance,
                PatientUsername = AnonymiseString(account.PatientUsername ?? "")
            });
        }
        return retVal;
    }

    public AccountResponse AnonymiseAccountResponse(AccountResponse account)
    {
        return new AccountResponse
        {
            Id = account.Id,
            AccountNumber = AnonymiseString(account.AccountNumber),
            Balance = account.Balance,
            PatientUsername = AnonymiseString(account.PatientUsername),
            PatientId = Guid.Parse(account.PatientId.ToString()),
        };
    }

    public AppointmentResponse AnonymiseAppointmentData(AppointmentResponse appointment)
    {
        return new AppointmentResponse
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            EndTime = appointment.EndTime,
            StartTime = appointment.StartTime,
            Price = appointment.Price,
            Report = AnonymiseString(appointment.Report ?? string.Empty),
            DoctorSpeciality = appointment.DoctorSpeciality,
            DoctorName = appointment.DoctorName,
        };
    }

    public AppointmentResponse AnonymiseAppointmentResponse(AppointmentResponse appointment)
    {
        return new AppointmentResponse
        {
            Id = appointment.Id,
            PatientId = Guid.Parse(appointment.PatientId.ToString()),
            DoctorId = Guid.Parse(appointment.DoctorId.ToString()),
            EndTime = appointment.EndTime,
            StartTime = appointment.StartTime,
            Price = AnonymiseString(appointment.Price ?? string.Empty),
            Report = AnonymiseString(appointment.Report ?? string.Empty),
            DoctorName = AnonymiseString(appointment.DoctorName),
            DoctorSpeciality = appointment.DoctorSpeciality,
            PatientName = AnonymiseString(appointment.PatientName),
        };
    }

    public AppointmentResponse AnonymiseAppointmentResponseForDoctor(AppointmentResponse appointment)
    {
        return new AppointmentResponse
        {
            Id = appointment.Id,
            PatientId = Guid.Parse(appointment.PatientId.ToString()),
            DoctorId = Guid.Parse(appointment.DoctorId.ToString()),
            EndTime = appointment.EndTime,
            StartTime = appointment.StartTime,
            Price = AnonymiseString(appointment.Price ?? string.Empty),
            Report = appointment.Report,
            DoctorName = appointment.DoctorName,
            DoctorSpeciality = appointment.DoctorSpeciality,
            PatientName = appointment.PatientName,
        };
    }

    public AccountantResponse AnonymiseAccountantData(Accountant accountant)
    {
        return new AccountantResponse
        {
            Id = accountant.Id.Value,
            Email = AnonymiseString(accountant.Email.Value),
            DateOfBirth = accountant.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            FirstName = AnonymiseString(accountant.FirstName.Value),
            Surname = AnonymiseString(accountant.Surname.Value),
            PersonalNumber = AnonymiseString(accountant.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(accountant.PhoneNumber.Value),
            Username = accountant.Username.Value
        };
    }

    public MultipleAppointmentsResponse AnonymiseAppointments(MultipleAppointmentsResponse appointments)
    {
        var response = new MultipleAppointmentsResponse();
        foreach (var appointment in appointments.Appointments)
        {
            response.Appointments.Add(AnonymiseAppointmentResponse(appointment));
        }
        return response;
    }

    public MultipleAppointmentsResponse AnonymiseAppointmentsForDoctor(MultipleAppointmentsResponse appointments)
    {
        var response = new MultipleAppointmentsResponse();
        foreach (var appointment in appointments.Appointments)
        {
            response.Appointments.Add(AnonymiseAppointmentResponseForDoctor(appointment));
        }
        return response;
    }

    public AccountantResponse AnonymiseAccountantResponse(AccountantResponse accountant)
    {
        return new AccountantResponse
        {
            Id = accountant.Id,
            Email = accountant.Email,
            DateOfBirth = accountant.DateOfBirth,
            FirstName = accountant.FirstName,
            Surname = accountant.Surname,
            PersonalNumber = AnonymiseString(accountant.PersonalNumber),
            PhoneNumber = accountant.PhoneNumber,
            Username = accountant.Username
        };
    }

    public AdminResponse AnonymiseAdminData(Admin admin)
    {
        return new AdminResponse
        {
            Id = admin.Id.Value,
            Email = AnonymiseString(admin.Email.Value),
            DateOfBirth = admin.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            FirstName = AnonymiseString(admin.FirstName.Value),
            Surname = AnonymiseString(admin.Surname.Value),
            PersonalNumber = AnonymiseString(admin.PersonalNumber.Value),
            PhoneNumber = AnonymiseString(admin.PhoneNumber.Value),
            Username = admin.Username.Value
        };
    }

    public AdminResponse AnonymiseAdminResponse(AdminResponse admin)
    {
        return new AdminResponse
        {
            Id = admin.Id,
            Email = AnonymiseString(admin.Email),
            DateOfBirth = admin.DateOfBirth,
            FirstName = AnonymiseString(admin.FirstName),
            Surname = AnonymiseString(admin.Surname),
            PersonalNumber = AnonymiseString(admin.PersonalNumber),
            PhoneNumber = AnonymiseString(admin.PhoneNumber),
            Username = admin.Username
        };
    }

    public GetAllAccountantsResponse AnonymiseAllAccountantsExceptCurrent(GetAllAccountantsResponse allAccountants, string username)
    {
        var response = new GetAllAccountantsResponse();
        foreach (var accountant in allAccountants.Accountants)
        {
            if (accountant.Username == username)
            {
                response.Accountants.Add(accountant);
            }
            response.Accountants.Add(AnonymiseAccountantResponse(accountant));
        }
        return response;
    }

    public GetAllAdminsResponse AnonymiseAllAdminsExceptCurrent(GetAllAdminsResponse allAdmins, string username)
    {
        var response = new GetAllAdminsResponse();
        foreach (var admin in allAdmins.Admins)
        {
            if (admin.Username == username)
            {
                response.Admins.Add(admin);
            }
            response.Admins.Add(AnonymiseAdminResponse(admin));
        }
        return response;
    }

    public GetAllDoctorsResponse AnonymiseAllDoctorsExceptCurrent(GetAllDoctorsResponse allDoctors, string username)
    {
        var response = new GetAllDoctorsResponse();
        foreach (var doctor in allDoctors.Doctors)
        {
            if (doctor.Username == username)
            {
                response.Doctors.Add(doctor);
                continue;
            }
            response.Doctors.Add(AnonymiseDoctorResponse(doctor, false));
        }
        return response;
    }

    public GetAllPatientsResponse AnonymiseAllPatientsExceptCurrent(GetAllPatientsResponse allPatients, string username)
    {
        var response = new GetAllPatientsResponse();
        foreach (var patient in allPatients.Patients)
        {
            if (patient.Username == username)
            {
                response.Patients.Add(patient);
            }
            response.Patients.Add(AnonymisePatientResponse(patient));
        }
        return response;
    }

    public GetAllDoctorsResponse AnonymiseAllDoctors(GetAllDoctorsResponse allDoctors, bool forAdmin)
    {
        var response = new GetAllDoctorsResponse();
        foreach (var doctor in allDoctors.Doctors)
        {
            response.Doctors.Add(AnonymiseDoctorResponse(doctor, forAdmin));
        }
        return response;
    }

    private string AnonymiseString(string report)
    {
        return new Regex("\\S").Replace(report, "*");
    }

    private string GetRandomFirstName()
    {
        if (Lorem.Chance(50, 100))
        {
            return Constants.MaleNames.ElementAt((int)Lorem.Number(0, Constants.MaleNames.Count - 1));
        }
        else
        {
            return Constants.FemaleNames.ElementAt((int)Lorem.Number(0, Constants.FemaleNames.Count - 1));
        }
    }

    private string GetRandomMaleFirstName()
    {
        return Constants.MaleNames.ElementAt((int)Lorem.Number(0, Constants.MaleNames.Count - 1));
    }

    private string GetRadnomFemaleFirstName()
    {
        return Constants.FemaleNames.ElementAt((int)Lorem.Number(0, Constants.FemaleNames.Count - 1));
    }

    private string GetRadnomSurname()
    {
        return Constants.Surnames.ElementAt((int)Lorem.Number(0, Constants.Surnames.Count - 1));
    }

    private string GetRandomPhoneNumber()
    {
        return $"+3816{gen.Next(100, 900)}{gen.Next(10, 99)}{gen.Next(100, 999)}";
    }

    private string GetRandomPersonalNumber()
    {
        return $"{gen.Next(100, 999)}{gen.Next(100, 999)}{gen.Next(100, 999)}{gen.Next(100, 999)}";
    }

    private string GetRandomUsername()
    {
        return $"{Lorem.Words(1, false)}{Lorem.Number(1, 100)}";
    }

    private string GetRandomAddress()
    {
        return $"{Constants.Towns.ElementAt((int)Lorem.Number(0, Constants.Towns.Count - 1))}," +
            $" {Constants.Streets.ElementAt((int)Lorem.Number(0, Constants.Streets.Count - 1))} {Lorem.Number(0, 50)}";
    }

    private double RandomDoubleValue()
    {
        Random rnd = new Random();
        return rnd.Next(-500, 500) * 100;
    }
}

