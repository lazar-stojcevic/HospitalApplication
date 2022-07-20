using HospitalApi.Domain.Common;

namespace HospitalApi.Domain;

public class Patient
{
    public PatientId Id { get; init; } = PatientId.From(Guid.NewGuid());

    public Username Username { get; init; } = default!;

    public FullName FullName { get; init; } = default!;

    public EmailAddress Email { get; init; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
}
