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
            FullName = patient.FullName.Value,
            DateOfBirth = patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        };
    }
}
