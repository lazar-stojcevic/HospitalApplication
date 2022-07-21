using System.Text.Json.Serialization;

namespace HospitalApi.Contracts.Data;
public class AccountDto
{
    [JsonPropertyName("pk")]
    public string Pk => Id;

    [JsonPropertyName("sk")]
    public string Sk => Id;
    public string Id { get; init; } = default!;
    public string AccountNumber { get; init; } = default!;
    public double Balance { get; set; } = default!;
    public string PatientId { get; init; } = default!;
}

