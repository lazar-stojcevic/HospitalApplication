using FastEndpoints;
using HospitalApi.Endpoints;

namespace HospitalApi.Summaries;

public class DeletePatientSummary : Summary<DeletePatientEndpoint>
{
    public DeletePatientSummary()
    {
        Summary = "Deleted a patient the system";
        Description = "Deleted a patient the system";
        Response(204, "The patient was deleted successfully");
        Response(404, "The patient was not found in the system");
    }
}
