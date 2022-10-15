using HospitalApi.Contracts.Data;
using HospitalApi.Domain;

namespace HospitalApi.Mapping;

public static class DomainToDtoMapper
{
    public static PatientDto ToPatientDto(this Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id.Value.ToString(),
            Email = patient.Email.Value,
            Username = patient.Username.Value,
            FirstName = patient.FirstName.Value,
            Surname = patient.Surname.Value,
            BloodType = patient.BloodType.Value,
            Weight = patient.Weight.Value,
            Height = patient.Height.Value,
            Adress = patient.Adress.Value,
            Gender = patient.Gender.Value,
            PersonalNumber = patient.PersonalNumber.Value,
            PhoneNumber = patient.PhoneNumber.Value,
            DateOfBirth = patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            AccountId = patient.AccountId?.ToString() ?? "",
            Password = patient.Password,
            IsActive = patient.IsActive
        };
    }

    public static AccountDto ToAccountDto(this Account account)
    {
        return new AccountDto
        {
            Id = account.Id.Value.ToString(),
            AccountNumber = account.AccountNumber.Value,
            Balance = account.Balance.Value,
            PatientId = account.PatientId.Value.ToString(),
        };
    }

    public static DoctorDto ToDoctorDto(this Doctor doctor)
    {
        return new DoctorDto
        {
            Id = doctor.Id.Value.ToString(),
            Email = doctor.Email.Value,
            Username = doctor.Username.Value,
            FirstName = doctor.FirstName.Value,
            Surname = doctor.Surname.Value,
            PersonalNumber = doctor.PersonalNumber.Value,
            PhoneNumber = doctor.PhoneNumber.Value,
            DateOfBirth = doctor.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            MedicalSpeciality = doctor.MedicalSpeciality.Value,
            Password = doctor.Password,
            IsActive = doctor.IsActive
        };
    }

    public static AccountantDto ToAccountantDto(this Accountant accountant)
    {
        return new AccountantDto
        {
            Id = accountant.Id.Value.ToString(),
            Email = accountant.Email.Value,
            Username = accountant.Username.Value,
            FirstName = accountant.FirstName.Value,
            Surname = accountant.Surname.Value,
            PersonalNumber = accountant.PersonalNumber.Value,
            PhoneNumber = accountant.PhoneNumber.Value,
            DateOfBirth = accountant.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            Password = accountant.Password
        };
    }

    public static AppointmentDto ToAppointmentDto(this Appointment appointmen)
    {
        return new AppointmentDto
        {
            Id = appointmen.Id.Value.ToString(),
            DoctorId = appointmen.DoctorId.Value.ToString(),
            PatientId = appointmen.PatientId.Value.ToString(),
            StartTime = appointmen.StartTime.Value,
            EndTime = appointmen.EndTime.Value,
            Report = appointmen.Report?.Value ?? string.Empty,
            Price = appointmen.Price?.Value ?? 0,
        };
    }

    public static AdminDto ToAdminDto(this Admin admin)
    {
        return new AdminDto
        {
            Id = admin.Id.Value.ToString(),
            Email = admin.Email.Value,
            Username = admin.Username.Value,
            FirstName = admin.FirstName.Value,
            Surname = admin.Surname.Value,
            PersonalNumber = admin.PersonalNumber.Value,
            PhoneNumber = admin.PhoneNumber.Value,
            DateOfBirth = admin.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            Password = admin.Password
        };
    }
}
