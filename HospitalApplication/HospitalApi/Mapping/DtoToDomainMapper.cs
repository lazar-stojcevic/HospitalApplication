using HospitalApi.Contracts.Data;
using HospitalApi.Domain;
using HospitalApi.Domain.Common.Financial;
using HospitalApi.Domain.Common.Patient;

namespace HospitalApi.Mapping;

public static class DtoToDomainMapper
{
    public static Patient ToPatient(this PatientDto patientDto)
    {
        return new Patient
        {
            Id = Domain.Common.Patient.PatientId.From(Guid.Parse(patientDto.Id)),
            Email = EmailAddress.From(patientDto.Email),
            Username = Username.From(patientDto.Username),
            FirstName = FirstName.From(patientDto.FirstName),
            Surname = Surname.From(patientDto.Surname),
            PhoneNumber = PhoneNumber.From(patientDto.PhoneNumber),
            PersonalNumber = PersonalNumber.From(patientDto.PersonalNumber),
            Gender = Gender.From(patientDto.Gender),
            Adress = Adress.From(patientDto.Adress),
            BloodType = BloodType.From(patientDto.BloodType),
            Height = Height.From(patientDto.Height),
            Weight = Weight.From(patientDto.Weight),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(patientDto.DateOfBirth))
        };
    }

    public static Account ToAccount(this AccountDto accountDto)
    {
        return new Account
        {
            Id = AccountId.From(Guid.Parse(accountDto.Id)),
            AccountNumber = AccountNumber.From(accountDto.AccountNumber),
            Balance = Balance.From(accountDto.Balance),
            PatientId = Domain.Common.Financial.PatientId.From(Guid.Parse(accountDto.PatientId)),
        }
    }
}
