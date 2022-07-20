using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Endpoints;

namespace HospitalApi.Summaries;

public class GetPatientSummary : Summary<GetPatientEndpoint>
{
    public GetPatientSummary()
    {
        Summary = "Returns a single patient by id";
        Description = "Returns a single patient by id";
        Response<GetAllPatientsResponse>(200, "Successfully found and returned the patients");
        Response(404, "The patient does not exist in the system");
    }
}
