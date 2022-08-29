using FastEndpoints;
using HospitalApi.Contracts.Requests.Admin;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Admin
{
    [HttpGet("admins/{id:guid}"), Authorize(Roles = "ACCOUNTANT,DOCTOR,ADMIN")]
    public class GetAdminEndpoint : Endpoint<GetAdminRequest, AdminResponse>
    {
        private readonly IAdminService _adminService;
        private readonly IAnonymizationService _anonymizationService;

        public GetAdminEndpoint(IAdminService adminService, IAnonymizationService anonymizationService)
        {
            _adminService = adminService;
            _anonymizationService = anonymizationService;
        }

        public override async Task HandleAsync(GetAdminRequest req, CancellationToken ct)
        {
            var admin = await _adminService.GetAsync(req.Id);

            if (admin is null)
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
            if (result.Equals("ADMIN"))
            {
                var adminResponse = admin.ToAdminResponse();
                await SendOkAsync(adminResponse, ct);
            }
            else if (result.Equals("ACCOUNTANT") || result.Equals("DOCTOR"))
            {
                var anonymisedData = _anonymizationService.AnonymiseAdminData(admin);
                await SendOkAsync(anonymisedData, ct);
            }
            else
            {
                await SendUnauthorizedAsync(ct);
            }
        }
    }
}
