using System.Text.Json.Serialization;

namespace HospitalApi.Contracts.Data;

public class AppointmentDto
{
    [JsonPropertyName("pk")]
    public string Pk => Id;

    [JsonPropertyName("sk")]
    public string Sk => Id;

    public string Id { get; init; } = default!;
    public string PatientId { get; init; } = null!;
    public string DoctorId { get; init; } = null!;
    public string Report { get; init; } = default!;
    public DateTime StartTime { get; init; } = default!;
    public DateTime EndTime { get; init; } = default!;
}

