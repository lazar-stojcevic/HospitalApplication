using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Endpoints.Appointment;

namespace HospitalApi.Summaries.Appointment;

public class FinishAppointmentSummary : Summary<FinishAppointmentEndpoint>
{
    public FinishAppointmentSummary()
    {
        Summary = "Updates existing appoitment in system";
        Description = "Updates existing appoitment in system";
        Response<AppointmentResponse>(200, "Appointment was successfully finished");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}

