﻿using FastEndpoints;
using HospitalApi.Contracts.Requests.Accountant;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Accountant;

[HttpGet("accountants/{id:guid}"), Authorize(Roles = "ACCOUNTANT,DOCTOR,ADMIN")]
public class GetAccountantEndpoint : Endpoint<GetAccountantRequest, AccountantResponse>
{
    private readonly IAccountantService _accountantService;
    private readonly IAnonymizationService _anonymizationService;

    public GetAccountantEndpoint(IAccountantService accountantService, IAnonymizationService anonymizationService)
    {
        _accountantService = accountantService;
        _anonymizationService = anonymizationService;
    }

    public override async Task HandleAsync(GetAccountantRequest req, CancellationToken ct)
    {
        var accountant = await _accountantService.GetAsync(req.Id);

        if (accountant is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var context = HttpContext;
        var result = string.Empty;
        if (context.User != null)
        {
            result = context.User.FindFirstValue(ClaimTypes.Role);
        }
        if (result.Equals("ACCOUNTANT"))
        {
            var accountantResponse = accountant.ToAccountantResponse();
            await SendOkAsync(accountantResponse, ct);
        }
        else if (result.Equals("ADMIN") || result.Equals("DOCTOR"))
        {
            var anonymisedData = _anonymizationService.AnonymiseAccountantData(accountant);
            await SendOkAsync(anonymisedData, ct);
        }
        else
        {
            await SendUnauthorizedAsync(ct);
        }
    }
}

