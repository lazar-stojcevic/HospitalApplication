using HospitalApi.Domain.Common.Doctor;
using HospitalApi.Domain.Common.Shared;

namespace HospitalApi.Domain;

public class Doctor
{
    public DoctorId Id { get; init; } = DoctorId.From(Guid.NewGuid());
    public Username Username { get; init; } = default!;
    public FirstName FirstName { get; init; } = default!;
    public Surname Surname { get; init; } = default!;
    public PersonalNumber PersonalNumber { get; init; } = default!;
    public PhoneNumber PhoneNumber { get; init; } = default!;
    public EmailAddress Email { get; init; } = default!;
    public DateOfBirth DateOfBirth { get; init; } = default!;
    public MedicalSpeciality MedicalSpeciality { get; init; } = default!;
    public string Password { get; set; } = default!;

    public void SetPassword(string password) => Password = password;
}

