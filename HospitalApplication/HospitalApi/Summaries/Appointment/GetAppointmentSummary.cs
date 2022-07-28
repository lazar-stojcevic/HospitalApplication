using FastEndpoints;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Endpoints.Appointment;

namespace HospitalApi.Summaries.Appointment
{
    public class GetAppointmentSummary : Summary<GetAppointmentEndpoint>
    {
        public GetAppointmentSummary()
        {
            Summary = "Returns a single appointment by id";
            Description = "Returns a single appointment by id";
            Response<AppointmentResponse>(200, "Successfully found and returned the appointment");
            Response(404, "The doctor does not exist in the system");
        }
    }
}
