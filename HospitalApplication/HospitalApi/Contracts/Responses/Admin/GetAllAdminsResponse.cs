namespace HospitalApi.Contracts.Responses.Admin;

public class GetAllAdminsResponse
{
    public IEnumerable<AdminResponse> Admins { get; init; } = Enumerable.Empty<AdminResponse>();
}

