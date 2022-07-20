namespace HospitalApi.Contracts.Requests;

public class CreatePatientRequest
{
    public string Username { get; init; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime DateOfBirth { get; init; }
}
