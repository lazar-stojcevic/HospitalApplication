using FastEndpoints;
using HospitalApi.Contracts.Requests.Financial;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Financial;

[HttpGet("accounts/{id:guid}"), Authorize(Roles = "PATIENT,ACCOUNTANT")]
public class GetAccountEndpoint : Endpoint<GetAccountRequest, AccountResponse>
{
    private readonly IAccountService _accountService;
    private readonly IAnonymizationService _anonymizationService;
    private readonly IPatientService _patientService;

    public GetAccountEndpoint(IAccountService accountService, IAnonymizationService anonymizationService, IPatientService patientService)
    {
        _accountService = accountService;
        _anonymizationService = anonymizationService;
        _patientService = patientService;
    }

    public override async Task HandleAsync(GetAccountRequest req, CancellationToken ct)
    {
        var patient = await _patientService.GetAsync(req.PatientId);
        if (patient is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var account = await _accountService.GetAsync(patient.Id.Value);
        if (account is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var username = string.Empty;
        var role = string.Empty;
        if (context.User != null)
        {
            username = context.User.FindFirstValue("Username");
            role = context.User.FindFirstValue(ClaimTypes.Role);
        }

        if (patient.Username.ToString().Equals(username) || role.Equals("ACCOUNTANT"))
        {
            await SendOkAsync(account.ToAccountResponse(), ct);
        }
        else
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
    }
}

