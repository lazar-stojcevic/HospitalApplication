using HospitalApi.Contracts.Requests;
using HospitalApi.Domain;
using HospitalApi.Domain.Common;

namespace HospitalApi.Mapping;

public static class ApiContractToDomainMapper
{
    public static Patient ToPatient(this CreatePatientRequest request)
    {
        return new Patient
        {
            Id = PatientId.From(Guid.NewGuid()),
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FirstName = FirstName.From(request.FirstName),
            Surname = Surname.From(request.Surname),
            Adress = Adress.From(request.Adress),
            BloodType = BloodType.From(request.BloodType),
            Gender = Gender.From(request.Gender),
            Height = Height.From(request.Height),
            Weight = Weight.From(request.Weight),
            PersonalNumber = PersonalNumber.From(request.PersonalNumber),
            PhoneNumber = PhoneNumber.From(request.PhoneNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }

    public static Patient ToPatient(this UpdatePatientRequest request)
    {
        return new Patient
        {
            Id = PatientId.From(request.Id),
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FirstName = FirstName.From(request.FirstName),
            Surname = Surname.From(request.Surname),
            Adress = Adress.From(request.Adress),
            BloodType = BloodType.From(request.BloodType),
            Gender = Gender.From(request.Gender),
            Height = Height.From(request.Height),
            Weight = Weight.From(request.Weight),
            PersonalNumber = PersonalNumber.From(request.PersonalNumber),
            PhoneNumber = PhoneNumber.From(request.PhoneNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }
}
