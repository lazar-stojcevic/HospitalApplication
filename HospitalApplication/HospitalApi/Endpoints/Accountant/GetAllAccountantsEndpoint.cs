using FastEndpoints;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Accountant;

[HttpGet("accountants"), Authorize(Roles = "ADMIN,DOCTOR,ACCOUNTANT")]
public class GetAllAccountantsEndpoint : Endpoint<EmptyRequest, GetAllAccountantsResponse>
{
    private readonly IAccountantService _accountantService;

    public GetAllAccountantsEndpoint(IAccountantService accountantService)
    {
        _accountantService = accountantService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var accountants = await _accountantService.GetAllAsync();

        if (accountants is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var accountantsResponse = accountants.ToAccountantsResponse();
        await SendOkAsync(accountantsResponse, ct);
    }
}

