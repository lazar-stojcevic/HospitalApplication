using HospitalApi.Contracts.Data;
using HospitalApi.Domain;
using HospitalApi.Domain.Common.Doctor;
using HospitalApi.Domain.Common.Financial;
using HospitalApi.Domain.Common.Patient;
using HospitalApi.Domain.Common.Shared;

namespace HospitalApi.Mapping;

public static class DtoToDomainMapper
{
    public static Patient ToPatient(this PatientDto patientDto)
    {
        return new Patient
        {
            Id = PatientId.From(Guid.Parse(patientDto.Id)),
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
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(patientDto.DateOfBirth)),
            AccountId = AccountId.From(Guid.Parse(patientDto.AccountId))
        };
    }

    public static Account ToAccount(this AccountDto accountDto)
    {
        return new Account
        {
            Id = AccountId.From(Guid.Parse(accountDto.Id)),
            AccountNumber = AccountNumber.From(accountDto.AccountNumber),
            Balance = Balance.From(accountDto.Balance),
            PatientId = PatientId.From(Guid.Parse(accountDto.PatientId)),
        };
    }

    public static Doctor ToDoctor(this DoctorDto doctorDto)
    {
        return new Doctor
        {
            Id = DoctorId.From(Guid.Parse(doctorDto.Id)),
            Email = EmailAddress.From(doctorDto.Email),
            Username = Username.From(doctorDto.Username),
            FirstName = FirstName.From(doctorDto.FirstName),
            Surname = Surname.From(doctorDto.Surname),
            PhoneNumber = PhoneNumber.From(doctorDto.PhoneNumber),
            PersonalNumber = PersonalNumber.From(doctorDto.PersonalNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(doctorDto.DateOfBirth)),
            MedicalSpeciality = MedicalSpeciality.From(doctorDto.MedicalSpeciality),
        };
    }
}
