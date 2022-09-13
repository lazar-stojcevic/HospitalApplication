using FastEndpoints;
using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Requests.Shared;
using HospitalApi.Contracts.Responses.Login;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Login;

[HttpPost("login"), AllowAnonymous]
public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginEndpoint(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var response = await _authenticationService.AuthenticateUser(new LoginDto{ 
            Username = req.Username,
            Password = req.Password
        });

        await SendOkAsync(response, ct);
    }
}

