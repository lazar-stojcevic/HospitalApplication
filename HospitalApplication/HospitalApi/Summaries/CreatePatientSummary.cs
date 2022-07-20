using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Endpoints;

namespace HospitalApi.Summaries;

public class CreatePatientSummary : Summary<CreatePatientEndpoint>
{
    public CreatePatientSummary()
    {
        Summary = "Creates a new patient in the system";
        Description = "Creates a new patient in the system";
        Response<PatientResponse>(201, "Patient was successfully created");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
