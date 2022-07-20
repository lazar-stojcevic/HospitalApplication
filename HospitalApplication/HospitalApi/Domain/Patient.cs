using HospitalApi.Domain.Common;

namespace HospitalApi.Domain;

public class Patient
{
    public PatientId Id { get; init; } = PatientId.From(Guid.NewGuid());
    public Username Username { get; init; } = default!;
    public FullName FullName { get; init; } = default!;
    public FirstName FirstName { get; init; } = default!;
    public Surname Surname { get; init; } = default!;
    public PersonalNumber PersonalNumber { get; init; } = default!;
    public Adress Adress { get; init; } = default!;
    public Gender Gender { get; init; } = default!;
    public Weight Weight { get; init; } = default!;
    public Height Height { get; init; } = default!;
    public BloodType BloodType { get; init; } = default!;
    public PhoneNumber PhoneNumber { get; init; } = default!;
    public EmailAddress Email { get; init; } = default!;
    public DateOfBirth DateOfBirth { get; init; } = default!;
}
