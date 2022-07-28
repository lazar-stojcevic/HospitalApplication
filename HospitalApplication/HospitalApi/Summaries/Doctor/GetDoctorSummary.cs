using FastEndpoints;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Endpoints.Doctor;

namespace HospitalApi.Summaries.Doctor;

public class GetDoctorSummary : Summary<GetDoctorEndpoint>
{
    public GetDoctorSummary()
    {
        Summary = "Returns a single doctor by id";
        Description = "Returns a single doctor by id";
        Response<DoctorResponse>(200, "Successfully found and returned the doctors");
        Response(404, "The doctor does not exist in the system");
    }
}
