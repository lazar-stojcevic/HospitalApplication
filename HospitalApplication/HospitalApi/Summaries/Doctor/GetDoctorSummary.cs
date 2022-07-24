using FastEndpoints;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Endpoints.Doctor;
using HospitalApi.Endpoints.Patient;

namespace HospitalApi.Summaries.Doctor;

public class GetDoctorSummary : Summary<GetDoctorEndpoint>
{
    public GetDoctorSummary()
    {
        Summary = "Returns a single doctor by id";
        Description = "Returns a single doctor by id";
        Response<GetAllPatientsResponse>(200, "Successfully found and returned the doctors");
        Response(404, "The doctor does not exist in the system");
    }
}
