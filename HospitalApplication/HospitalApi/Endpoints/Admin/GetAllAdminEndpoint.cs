using FastEndpoints;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Admin
{
    [HttpGet("admins"), Authorize(Roles = "ADMIN,DOCTOR,ACCOUNTANT")]
    public class GetAllAdminEndpoint : Endpoint<EmptyRequest, GetAllAdminsResponse>
    {
        private readonly IAdminService _adminService;

        public GetAllAdminEndpoint(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            var admins = await _adminService.GetAllAsync();

            if (admins is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var adminsResponse = admins.ToAdminsResponse();
            await SendOkAsync(adminsResponse, ct);
        }
    }
}
