namespace HospitalApi.Contracts.Requests.Financial;

public class ChangeAccountBalanceRequest
{
    public Guid Id { get; init; }
    public double Change { get; init; } = default!;
}

