namespace HospitalApi.Contracts.Requests.Shared;

public class LoginRequest
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}

