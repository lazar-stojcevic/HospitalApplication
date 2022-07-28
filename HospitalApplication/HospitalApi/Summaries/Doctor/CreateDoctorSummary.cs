using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Endpoints.Doctor;

namespace HospitalApi.Summaries.Doctor;

public class CreateDoctorSummary : Summary<CreateDoctorEndpoint>
{
    public CreateDoctorSummary()
    {
        Summary = "Creates a new doctor in the system";
        Description = "Creates a new doctor in the system";
        Response<DoctorResponse>(201, "Doctor was successfully created");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
