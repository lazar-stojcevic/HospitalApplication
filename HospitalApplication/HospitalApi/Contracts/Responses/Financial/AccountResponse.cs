namespace HospitalApi.Contracts.Responses.Financial;

public class AccountResponse
{
    public Guid Id { get; init; }
    public string AccountNumber { get; init; } = default!;
    public double Balance { get; init; } = default!;
    public Guid PatientId { get; init; }
}

