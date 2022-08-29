using FastEndpoints;
using HospitalApi.Contracts.Requests.Accountant;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Domain.Types;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Accountant;

[HttpPost("accountants"), Authorize(Roles = "ADMIN")]
public class CreateAccountantEndpoint : Endpoint<CreateAccountantRequest, AccountantResponse>
{
    private readonly IAccountantService _accountantService;
    private readonly IAuthenticationService _authenticationService;

    public CreateAccountantEndpoint(IAccountantService accountantService, IAuthenticationService authenticationService)
    {
        _accountantService = accountantService;
        _authenticationService = authenticationService;
    }

    public override async Task HandleAsync(CreateAccountantRequest req, CancellationToken ct)
    {
        if (!await _authenticationService.IsUsernameUnique(req.Username, UserType.Accountant))
        {
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        var accountant = req.ToAccountant();

        accountant.SetPassword(_authenticationService.HashPassword(req.Password));

        await _accountantService.CreateAsync(accountant);

        var accountantResponse = accountant.ToAccountantResponse();
        await SendCreatedAtAsync<GetAccountantEndpoint>(
            new { Id = accountant.Id.Value }, accountantResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}

