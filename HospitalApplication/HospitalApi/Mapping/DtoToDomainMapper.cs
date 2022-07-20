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
            FullName = FullName.From(patientDto.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(patientDto.DateOfBirth))
        };
    }
}
