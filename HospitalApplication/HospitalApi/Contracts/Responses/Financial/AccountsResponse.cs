namespace HospitalApi.Contracts.Responses.Financial
{
    public class AccountsResponse
    {
        public ICollection<AccountResponse> Accounts { get; set; } = new List<AccountResponse>();
    }
}
