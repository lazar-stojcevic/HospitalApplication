using HospitalApi.Endpoints;
using FastEndpoints;
using HospitalApi.Contracts.Responses;

namespace HospitalApi.Summaries;

public class UpdatePatientSummary : Summary<UpdatePatientEndpoint>
{
    public UpdatePatientSummary()
    {
        Summary = "Updates an existing patient in the system";
        Description = "Updates an existing patient in the system";
        Response<PatientResponse>(201, "Patient was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
