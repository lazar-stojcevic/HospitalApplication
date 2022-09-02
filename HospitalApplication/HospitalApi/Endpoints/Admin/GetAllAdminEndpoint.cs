using FastEndpoints;
using HospitalApi.Contracts.Responses.Admin;
using HospitalApi.Mapping;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HospitalApi.Endpoints.Admin
{
    [HttpGet("admins"), Authorize(Roles = "ADMIN,DOCTOR,ACCOUNTANT")]
    public class GetAllAdminEndpoint : Endpoint<EmptyRequest, GetAllAdminsResponse>
    {
        private readonly IAdminService _adminService;
        private readonly IAnonymizationService _anonymizationService;

        public GetAllAdminEndpoint(IAdminService adminService, IAnonymizationService anonymizationService)
        {
            _adminService = adminService;
            _anonymizationService = anonymizationService;
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            var admins = await _adminService.GetAllAsync();

            if (admins is null)
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

            var adminsResponse = admins.ToAdminsResponse();
            await SendOkAsync(_anonymizationService.AnonymiseAllAdminsExceptCurrent(adminsResponse, result), ct);
        }
    }
}
