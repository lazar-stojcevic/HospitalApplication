using HospitalApi.Contracts.Responses;
using HospitalApi.Domain;

namespace HospitalApi.Mapping;

public static class DomainToApiContractMapper
{
    public static PatientResponse ToPatientResponse(this Patient patient)
    {
        return new PatientResponse
        {
            Id = patient.Id.Value,
            Email = patient.Email.Value,
            Username = patient.Username.Value,
            FullName = patient.FullName.Value,
            DateOfBirth = patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        };
    }

    public static GetAllPatientsResponse ToPatientsResponse(this IEnumerable<Patient> patients)
    {
        return new GetAllPatientsResponse
        {
            Patients = patients.Select(x => new PatientResponse
            {
                Id = x.Id.Value,
                Email = x.Email.Value,
                Username = x.Username.Value,
                FullName = x.FullName.Value,
                DateOfBirth = x.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
            })
        };
    }
}
