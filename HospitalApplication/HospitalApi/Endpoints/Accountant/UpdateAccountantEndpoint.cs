using FastEndpoints;
using HospitalApi.Contracts.Requests.Accountant;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Accountant;

[HttpPut("accountants/{id:guid}"), Authorize(Roles = "ACCOUNTANT")]
public class UpdateAccountantEndpoint : Endpoint<UpdateAccountantRequest, AccountantResponse>
{
    private readonly IAccountantService _accoutantService;

    public UpdateAccountantEndpoint(IAccountantService accoutantService)
    {
        _accoutantService = accoutantService;
    }

    public override async Task HandleAsync(UpdateAccountantRequest req, CancellationToken ct)
    {
        var context = HttpContext;
        var username = string.Empty;
        if (context.User != null)
        {
            username = context.User.FindFirstValue(ClaimTypes.Name);
        }

        var existingAccountant = await _accoutantService.GetAsync(Guid.Parse(req.Id));

        if (existingAccountant == null)
        {
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        if (!existingAccountant.Username.Equals(username))
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var accountant = req.ToAccountant();
        accountant.SetPassword(existingAccountant.Password);
        await _accoutantService.UpdateAsync(accountant);

        var accountantResponse = accountant.ToAccountantResponse();
        await SendOkAsync(accountantResponse, ct);
    }
}

