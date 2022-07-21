namespace HospitalApi.Contracts.Responses.Financial
{
    public class GetAllAccountsResponse
    {
        public IEnumerable<AccountResponse> Accounts { get; init; } = Enumerable.Empty<AccountResponse>();
    }
}
