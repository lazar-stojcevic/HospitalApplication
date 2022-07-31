using HospitalApi.Domain.Common.Financial;
using HospitalApi.Domain.Common.Patient;
using HospitalApi.Domain.Common.Shared;

namespace HospitalApi.Domain;

public class Patient
{
    public PatientId Id { get; init; } = PatientId.From(Guid.NewGuid());
    public Username Username { get; init; } = default!;
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
    public string Password { get; private set; } = default!;


    public AccountId AccountId { get; set; } = default!;

    public AccountId SetAccountId(Guid id) => AccountId = AccountId.From(id);

    public void SetPassword(string password) => Password = password;
}
