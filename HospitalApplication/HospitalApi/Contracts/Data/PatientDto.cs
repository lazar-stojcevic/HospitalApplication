using System.Text.Json.Serialization;

namespace HospitalApi.Contracts.Data;

public class PatientDto
{
    [JsonPropertyName("pk")]
    public string Pk => Id;

    [JsonPropertyName("sk")]
    public string Sk => Id;

    public string Id { get; init; } = default!;

    public string Username { get; init; } = default!;
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
    public bool IsActive { get; set; }
    public string Password { get; init; } = default!;

    public string AccountId { get; set; } = default!;
}
