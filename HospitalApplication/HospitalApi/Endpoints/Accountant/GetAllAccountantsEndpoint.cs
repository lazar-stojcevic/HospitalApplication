using FastEndpoints;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Accountant;

[HttpGet("accountants"), Authorize(Roles = "ADMIN,DOCTOR,ACCOUNTANT")]
public class GetAllAccountantsEndpoint : Endpoint<EmptyRequest, GetAllAccountantsResponse>
{
    private readonly IAccountantService _accountantService;
    private readonly IAnonymizationService _anonymizationService;

    public GetAllAccountantsEndpoint(IAccountantService accountantService, IAnonymizationService anonymizationService)
    {
        _accountantService = accountantService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var accountants = await _accountantService.GetAllAsync();

        if (accountants is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var context = HttpContext;
        var result = string.Empty;
        if (context.User != null)
        {
            result = context.User.FindFirstValue(ClaimTypes.Name);
        }

        var accountantsResponse = accountants.ToAccountantsResponse();
        await SendOkAsync(_anonymizationService.AnonymiseAllAccountantsExceptCurrent(accountantsResponse, result), ct);
    }
}

