using FastEndpoints;
using HospitalApi.Contracts.Requests.Accountant;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Accountant;

[HttpGet("accountants/{id:guid}"), AllowAnonymous]
public class GetAccountantEndpoint : Endpoint<GetAccountantRequest, AccountantResponse>
{
    private readonly IAccountantService _accountantService;

    public GetAccountantEndpoint(IAccountantService accountantService)
    {
        _accountantService = accountantService;
    }

    public override async Task HandleAsync(GetAccountantRequest req, CancellationToken ct)
    {
        var accountant = await _accountantService.GetAsync(req.Id);

        if (accountant is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var accountantResponse = accountant.ToAccountantResponse();
        await SendOkAsync(accountantResponse, ct);
    }
}

