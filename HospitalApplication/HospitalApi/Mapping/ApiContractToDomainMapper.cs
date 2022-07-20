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
            FullName = FullName.From(request.FullName),
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
            FullName = FullName.From(request.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }
}
