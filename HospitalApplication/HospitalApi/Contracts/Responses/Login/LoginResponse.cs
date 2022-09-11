namespace HospitalApi.Contracts.Responses.Login;

public class LoginResponse
{
    public string Token { get; init; } = default!;
    public string Role { get; init; } = default!;
}

