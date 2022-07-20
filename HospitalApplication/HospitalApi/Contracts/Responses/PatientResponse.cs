namespace HospitalApi.Contracts.Responses;

public class PatientResponse
{
    public Guid Id { get; init; }

    public string Username { get; init; } = default!;

    public string FullName { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string Surname { get; init; } = default!;
    public string PersonalNumber { get; init; } = default!;
    public string Adress { get; init; } = default!;
    public string Gender { get; init; } = default!;
    public double Weight { get; init; } = default!;
    public double Height { get; init; } = default!;
    public string BloodType { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
    public string Email { get; init; } = default!;
    public DateTime DateOfBirth { get; init; }
}
