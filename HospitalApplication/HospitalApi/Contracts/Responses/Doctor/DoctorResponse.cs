namespace HospitalApi.Contracts.Responses.Doctor;

public class DoctorResponse
{
    public Guid Id { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string Surname { get; init; } = default!;
    public string PersonalNumber { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
    public string Email { get; init; } = default!;
    public DateTime DateOfBirth { get; init; } = default!;
    public string MedicalSpeciality { get; init; } = default!;
    public bool IsActive { get; init; } = default!;
}

