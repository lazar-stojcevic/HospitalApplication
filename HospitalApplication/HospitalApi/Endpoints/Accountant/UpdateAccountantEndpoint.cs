using FastEndpoints;
using HospitalApi.Contracts.Requests.Accountant;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Accountant;

[HttpPut("accountants/{id:guid}"), AllowAnonymous]
public class UpdateAccountantEndpoint : Endpoint<UpdateAccountantRequest, AccountantResponse>
{
    private readonly IAccountantService _accoutantService;

    public UpdateAccountantEndpoint(IAccountantService accoutantService)
    {
        _accoutantService = accoutantService;
    }

    public override async Task HandleAsync(UpdateAccountantRequest req, CancellationToken ct)
    {
        var existingAccountant = await _accoutantService.GetAsync(Guid.Parse(req.Id));

        if (existingAccountant is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var accountant = req.ToAccountant();
        accountant.SetPassword(existingAccountant.Password);
        await _accoutantService.UpdateAsync(accountant);

        var accountantResponse = accountant.ToAccountantResponse();
        await SendOkAsync(accountantResponse, ct);
    }
}

