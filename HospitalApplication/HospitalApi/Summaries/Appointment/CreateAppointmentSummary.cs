using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Endpoints.Appointment;

namespace HospitalApi.Summaries.Appointment;

public class CreateAppointmentSummary : Summary<CreateAppointmentEndpoint>
{
    public CreateAppointmentSummary()
    {
        Summary = "Creates a new appointment in the system";
        Description = "Creates a new appointment in the system";
        Response<AppointmentResponse>(201, "Appointment was successfully created");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}

