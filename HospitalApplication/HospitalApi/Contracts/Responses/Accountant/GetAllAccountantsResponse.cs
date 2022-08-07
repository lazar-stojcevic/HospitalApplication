namespace HospitalApi.Contracts.Responses.Accountant;

public class GetAllAccountantsResponse
{
    public IEnumerable<AccountantResponse> Accountants { get; init; } = Enumerable.Empty<AccountantResponse>();
}

