using FastEndpoints;
using HospitalApi.Contracts.Requests.Admin;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Admin
{
    [HttpPut("admins/{id:guid}"), Authorize(Roles = "ADMIN")]
    public class UpdateAdminEndpoint : Endpoint<UpdateAdminRequest, AdminResponse>
    {
        private readonly IAdminService _adminService;

        public UpdateAdminEndpoint(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public override async Task HandleAsync(UpdateAdminRequest req, CancellationToken ct)
        {
            var context = HttpContext;
            var username = string.Empty;
            if (context.User != null)
            {
                username = context.User.FindFirstValue(ClaimTypes.Name);
            }

            var existingAdmin = await _adminService.GetAsync(Guid.Parse(req.Id));

            if (existingAdmin == null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            if (!existingAdmin.Username.Equals(username))
            {
                await SendForbiddenAsync(ct);
                return;
            }

            var admin = req.ToAdmin();
            admin.SetPassword(existingAdmin.Password);
            await _adminService.UpdateAsync(admin);

            var adminResponse = admin.ToAdminResponse();
            await SendOkAsync(adminResponse, ct);
        }
    }
}
