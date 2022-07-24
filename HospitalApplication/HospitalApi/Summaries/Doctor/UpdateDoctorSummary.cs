using FastEndpoints;
using HospitalApi.Contracts.Responses;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Endpoints.Doctor;
using HospitalApi.Endpoints.Patient;

namespace HospitalApi.Summaries.Doctor;

public class UpdateDoctorSummary : Summary<UpdateDoctorEndpoint>
{
    public UpdateDoctorSummary()
    {
        Summary = "Updates an existing doctor in the system";
        Description = "Updates an existing doctor in the system";
        Response<PatientResponse>(201, "Doctor was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
