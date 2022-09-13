namespace HospitalApi.Contracts.Responses.Admin;

public class GetAllAdminsResponse
{
    public ICollection<AdminResponse> Admins { get; init; } = new List<AdminResponse>();
}

