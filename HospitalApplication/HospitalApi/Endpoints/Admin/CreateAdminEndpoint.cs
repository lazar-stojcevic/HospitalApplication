using FastEndpoints;
using HospitalApi.Contracts.Requests.Admin;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Domain.Types;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Admin;
[HttpPost("admins"), Authorize(Roles = "ADMIN")]
public class CreateAdminEndpoint : Endpoint<CreateAdminRequest, AdminResponse>
{
    private readonly IAdminService _adminService;
    private readonly IAuthenticationService _authenticationService;

    public CreateAdminEndpoint(IAdminService adminService, IAuthenticationService authenticationService)
    {
        _adminService = adminService;
        _authenticationService = authenticationService;
    }

    public override async Task HandleAsync(CreateAdminRequest req, CancellationToken ct)
    {
        if (!await _authenticationService.IsUsernameUnique(req.Username, UserType.Admin))
        {
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        var admin = req.ToAdmin();

        admin.SetPassword(_authenticationService.HashPassword(req.Password));

        await _adminService.CreateAsync(admin);

        var adminResponse = admin.ToAdminResponse();
        await SendCreatedAtAsync<GetAdminEndpoint>(
            new { Id = admin.Id.Value }, adminResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}

