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
        };
    }
}
