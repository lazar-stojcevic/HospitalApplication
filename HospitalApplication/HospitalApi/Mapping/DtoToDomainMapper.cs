using HospitalApi.Contracts.Data;
using HospitalApi.Domain;
using HospitalApi.Domain.Common;

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
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(patientDto.DateOfBirth))
        };
    }
}
