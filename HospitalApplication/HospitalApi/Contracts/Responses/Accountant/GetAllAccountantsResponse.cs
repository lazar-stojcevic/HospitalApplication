namespace HospitalApi.Contracts.Responses.Accountant;

public class GetAllAccountantsResponse
{
    public ICollection<AccountantResponse> Accountants { get; init; } = new List<AccountantResponse>();
}

