using FastEndpoints;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Endpoints.Doctor
{
    [HttpPut("doctors/unblock/{id:guid}"), Authorize(Roles = "ADMIN")]
    public class UnblockDoctorEndpoint : Endpoint<UnblockDoctorRequest, bool>
    {
        private readonly IDoctorService _doctorService;

        public UnblockDoctorEndpoint(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public override async Task HandleAsync(UnblockDoctorRequest req, CancellationToken ct)
        {
            var doctor = await _doctorService.GetAsync(req.Id, true);

            if (doctor is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            doctor.IsActive = true;
            doctor.SetPassword(doctor.Password);
            await _doctorService.UpdateAsync(doctor);

            await SendOkAsync(true, ct);
        }
    }
}
