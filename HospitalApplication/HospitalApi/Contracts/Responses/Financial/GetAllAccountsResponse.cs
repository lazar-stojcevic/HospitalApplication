namespace HospitalApi.Contracts.Responses.Financial
{
    public class GetAllAccountsResponse
    {
        public ICollection<AccountResponse> Accounts { get; init; } = new List<AccountResponse>();
    }
}
