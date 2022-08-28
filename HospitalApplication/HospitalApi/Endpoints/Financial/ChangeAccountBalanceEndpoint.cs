using FastEndpoints;
using HospitalApi.Contracts.Requests.Financial;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Financial;

[HttpPut("account/change"), Authorize(Roles = "ACCOUNTANT")]
public class ChangeAccountBalanceEndpoint : Endpoint<ChangeAccountBalanceRequest, bool>
{
    private readonly IAccountService _accountService;

    public ChangeAccountBalanceEndpoint(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public override async Task HandleAsync(ChangeAccountBalanceRequest req, CancellationToken ct)
    {
        var existingAccount = await _accountService.GetAsync(req.Id);

        if (existingAccount is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _accountService.UpdateAsync(existingAccount, req.Change);

        await SendOkAsync(true, ct);
    }
}

