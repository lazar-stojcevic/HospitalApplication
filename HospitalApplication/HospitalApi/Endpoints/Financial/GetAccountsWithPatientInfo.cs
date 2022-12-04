using FastEndpoints;
using HospitalApi.Contracts.Requests.Financial;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Financial;

[HttpGet("accounts"), Authorize(Roles = "ACCOUNTANT,ADMIN")]
public class GetAccountsWithPatientInfo : Endpoint<GetAccountsRequest, AccountsResponse>
{
    private readonly IAccountService _accountService;
    private readonly IAnonymizationService _anonymizationService;
    private readonly IPatientService _patientService;

    public GetAccountsWithPatientInfo(IAccountService accountService, IAnonymizationService anonymizationService, IPatientService patientService)
    {
        _accountService = accountService;
        _anonymizationService = anonymizationService;
        _patientService = patientService;
    }

    public override async Task HandleAsync(GetAccountsRequest req, CancellationToken ct)
    {
        var patients = await _patientService.GetAllAsync();
        var accounts = await _accountService.GetAllAsync();

        var context = HttpContext;
        var result = string.Empty;
        var role = string.Empty;
        if (context.User != null)
        {
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }

        var retVal = new AccountsResponse();

        retVal.Accounts = accounts!.Select(x => new AccountResponse()
        {
            Id = x.Id.Value,
            AccountNumber = x.AccountNumber.Value,
            Balance = x.Balance.Value,
            PatientId = x.PatientId.Value,
            PatientUsername = patients!.Where(p => p.AccountId.Value.Equals(x.Id.Value)).Select(p => p.Username.Value).FirstOrDefault(),
        }).ToList();

        if (role.Equals("ADMIN"))
        {
            retVal = _anonymizationService.AnonymiseMultipleAccounts(retVal);
        }
        else
        {
            retVal = _anonymizationService.AnonymiseMultipleAccountsForAccountant(retVal);
        }

        await SendOkAsync(retVal, ct);
    }
}

